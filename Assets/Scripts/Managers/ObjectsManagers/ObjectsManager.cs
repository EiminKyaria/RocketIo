using System.Collections.Generic;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Managers.ObjectsManagers
{

    /// <summary>
    /// Mother class of managers which manage physics object
    /// </summary>
    public class PhysicsManager<T0,T1> : BaseManager<T0> where T0 : Component
    {
        #region Properties
        /// <summary>
        ///list of object use by manager 
        /// </summary>
        public List<T1> objectsList
        {
            get;

            protected set;
        }
        #endregion

        #region Monobehaviour's functions
        override protected void Awake()
        {
            objectsList = new List<T1>();
            base.Awake();
        }

        virtual protected void Start()
        {

        }

        virtual protected void OnDestroy()
        {

        }


        #endregion

        #region Listener's functions
        
        #endregion

    }
}