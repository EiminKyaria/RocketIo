using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours
{
    public class MonoBehaviourLegacy:MonoBehaviour
    {
        #region Debug

        [Header("Débug")]
        public bool DebugMode = false;

        #endregion

        #region Private Variable

        protected Transform m_transform;

        public Transform MyTransform
        {
            get { return m_transform; }
        }

        #endregion

        #region MonoBehaviour's functions
        protected virtual void Awake()
        {
            m_transform = GetComponent<Transform>();
        }

        protected virtual void Start()
        {

        }
        
        protected virtual void Update()
        {
           

        }

        protected virtual void OnDestroy()
        {

        }

        #endregion
    }
}
