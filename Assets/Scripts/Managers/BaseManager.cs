using Com.Eimin.Personnal.Scripts.Utils.Classes;
using Random = UnityEngine.Random;

namespace Com.Eimin.Personnal.Scripts.Managers
{
    using System;
    using UnityEngine;

    public class BaseManager<T> : MonoBehaviour
        where T : Component
    {
        #region Debug

        [Header("Débug")]
        public bool DebugMode = false;

        #endregion

        private static T _instance;

        public static T instance
        {
            get { return _instance; }
        }

        #region Init Variable

        protected bool m_isStart;
        protected bool m_isInit;

        public bool IsStart
        {
            get { return m_isStart; }
        }

        public bool IsInit
        {
            get { return m_isInit; }
        }

        #endregion


        [Header("Singleton")]
        [SerializeField] private bool replaceIfAlwaysExist = true;
        [SerializeField] private bool dontDestroyGameObjectOnLoad = true;

        protected virtual void Awake()
        {
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

        protected virtual void Start()
        {
        }


        protected virtual void ListenerGameState(GameState pState)
        {
        }
    }
}