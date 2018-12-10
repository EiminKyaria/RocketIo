using UnityEngine;
using System;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours
{

    /// <summary>
    /// 
    /// </summary>
    public class MonoBehaviorLegacySingleton<T> : MonoBehaviourLegacy where T:MonoBehaviour
    {

        protected static T _instance;

        /// <summary>
        /// unique instance of the class
        /// </summary>
        public static T instance
        {
            get
            {
                return _instance;
            }
        }

        [Header("Singleton")]
        [SerializeField]
        protected bool replaceIfAlwaysExist = true;
        [SerializeField]
        protected bool dontDestroyGameObjectOnLoad = true;



        override protected void Awake()
        {
            base.Awake();

            string className = "" + GetType();
            className = className.Substring(className.LastIndexOf('.')).Replace(".", "");

            string throwMessage = "Attempting to create another instance of " + className + " while it is a singleton.";

            if (_instance != null)
            {
                if (replaceIfAlwaysExist)
                {
                    Destroy(instance.gameObject);
                    throwMessage += " We have replaced the old version by this one";
                }
                else
                {
                    Destroy(gameObject);
                    throwMessage += " We have destroyed this version";
                }
                
            }

            if (replaceIfAlwaysExist || (!replaceIfAlwaysExist && _instance == null))
                _instance = this as T;

            if (dontDestroyGameObjectOnLoad) DontDestroyOnLoad(gameObject);
        }

        override protected void OnDestroy()
        {
            base.OnDestroy();
            _instance = null;
        }
    }
}