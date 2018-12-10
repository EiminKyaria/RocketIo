using System;
using UnityEngine;
using System.Collections.Generic;

namespace Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers
{

    [Serializable]
    public class PoolingCarac
    {
        public GameObject objectToCreate;
        public int maxToCreate;
        public Transform ContainerToInstantiate;

    }
    /// <summary>
    /// manage of pooling
    /// </summary>
    public class PoolingManager : BaseManager<PoolingManager>
    {
        #region Properties
        /// <summary>
        ///Dictionary of pool 
        /// </summary>
        protected Dictionary<string, List<GameObject>> poolArray;

        /// <summary>
        ///list of object use by pooling 
        /// </summary>
        [SerializeField]
        List<PoolingCarac> objectsList;
        #endregion

        #region Monobehaviour's functions
        protected override void Awake()
        {
            base.Awake();
            poolArray = new Dictionary<string, List<GameObject>>();
        }

        protected override void Start()
        {
            base.Start();
            CreatePool();
        }

        #endregion

        #region Pool functions
        /// <summary>
        ///create the pool and object at start 
        /// </summary>
        protected void CreatePool()
        {
            PoolingCarac cPool;

            for (int i = 0; i < objectsList.Count; i++)
            {
                cPool = objectsList[i];
                for (int j = 0; j < cPool.maxToCreate; j++)
                {
                    if (!poolArray.ContainsKey(cPool.objectToCreate.name)) poolArray[cPool.objectToCreate.name] = new List<GameObject>();
                    GameObject myObject;
                    if (cPool.ContainerToInstantiate != null)
                    {
                        myObject = Instantiate(cPool.objectToCreate, cPool.ContainerToInstantiate);
                    }
                    else
                    {
                        myObject = Instantiate(cPool.objectToCreate);
                    }
                    poolArray[cPool.objectToCreate.name].Add(myObject);
                    myObject.SetActive(false);
                }

            }
        }

        /// <summary>
        ///give a pool object 
        /// </summary>
        /// <param name="pObject">the type of object to get</param>
        /// <returns>the object want</returns>
        public GameObject GetFromPool(GameObject pObject)
        {
            List<GameObject> pList = poolArray[pObject.name];
            GameObject myObject;
            PoolingCarac cPool;
            if (pList.Count == 0)
            {
                cPool = GetPoolCarac(pObject);
                if (cPool != null)
                {
                    if (cPool.ContainerToInstantiate != null)
                    {
                        myObject = Instantiate(pObject, cPool.ContainerToInstantiate);

                    }
                    else
                    {
                        myObject = Instantiate(pObject);

                    }
                }
                else myObject = null;
            }
            else
            {
                myObject = pList[0];
                pList.Remove(myObject);
                poolArray[pObject.name] = pList;
            }

            myObject.SetActive(true);
            return myObject;
        }


        /// <summary>
        ///add an object to the pool 
        /// </summary>
        /// <param name="pObject">the object to add</param>
        public void AddToPool(GameObject pObject)
        {
            string pName = pObject.gameObject.name.Replace("(Clone)", "");

            poolArray[pName].Add(pObject);

            pObject.SetActive(false);

        }

        /// <summary>
        /// get the pooling info of a prefab 
        /// </summary>
        /// <param name="pObject"></param>
        /// <returns></returns>
        public PoolingCarac GetPoolCarac(GameObject pObject)
        {
            foreach (PoolingCarac cPool in objectsList)
            {
                if (cPool.objectToCreate == pObject) return cPool;
            }
            return null;
        }
        #endregion
    }
}