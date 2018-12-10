using System.Collections;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Game.Monobehaviours;
using Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Utils.Tools
{
    public class MyCamera : MonoBehaviourLegacy
    {
        #region Editor Variables

        [SerializeField] private Transform m_camera;
        [SerializeField] private Transform m_Player;
        [SerializeField] private Transform m_Ballon;
        [SerializeField] private Transform m_Arrow;

        [SerializeField] private float m_minDistance = 5f;

        [SerializeField] private float m_maxDistance = 50f;

        [SerializeField] private float m_maxScaleVector = 10f;

        #endregion

        #region Private Variables

        private float m_distance;
        private float m_compense;
        private float m_base_z;

        private float m_compensePercent;

        private Vector3 m_ScaleCompense;

        #endregion

        #region MonoBehaviour's functions

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            m_base_z = m_camera.position.z;
            m_ScaleCompense = Vector3.one * m_maxScaleVector;
        }

        protected override void Update()
        {
            base.Update();

            if (!GameManager.instance.isInGame) return;

            m_distance = Vector3.Distance(m_Player.position, m_Ballon.position);

            m_compense = m_distance ;
            m_compense = Mathf.Clamp(m_compense, 0, m_maxDistance);

            m_compensePercent = m_compense / m_maxDistance;

        }

        protected void LateUpdate()
        {
            if (!GameManager.instance.isInGame) return;
            
            m_camera.localPosition = Vector3.Lerp(m_camera.localPosition ,new Vector3(
                m_camera.localPosition.x,
                m_camera.localPosition.y,
                m_base_z - m_compense ), 0.75f);


            m_Arrow.localScale = Vector3.one + m_ScaleCompense * m_compensePercent;
        }


       

        #endregion
    }
}