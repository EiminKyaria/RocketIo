using System.Collections;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours
{
    public class Enemy : Mobile
    {

        #region Editor Variable

        [SerializeField] private float m_rotLerp = 1f;

        [Header("Tagets Param")]
        [SerializeField]
        private Transform m_target;
        [SerializeField]
        private Transform m_cage;

        [Header("ShootParam")]
        [SerializeField] private float m_minDirectionToShoot = 6f;

        #endregion

        #region Private Variable

        private Vector3 m_newRot;
        private Vector3 m_dir;
        private Vector3 m_targetPoint;

        private Vector3 m_cageDir;

        private bool shoot;

        #endregion

        #region MonoBehaviour's functions
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            m_type = MobileType.Enemy;
            m_speedCoef = 0.5f;
           
            if(GameManager.instance.IsNewMatch)m_graphic.SetActive(false);
        }

        protected override void Update()
        {
            base.Update();
        }

        #endregion

        #region Normal Action

        protected override void SetModeNormal()
        {
            base.SetModeNormal();
        }

        protected override void DoActionNormal()
        {
            if (!GameManager.instance.isInGame) return;
            if(!m_graphic.activeInHierarchy)m_graphic.SetActive(true);

            base.DoActionNormal();

            GetRotation();
            Move();
        }

        #endregion

        #region Overrided Function

        protected override void GetRotation()
        {
            base.GetRotation();

            GetDirection();
            m_cageDir = m_targetPoint - m_transform.position;
            m_cageDir.y = 0;

            if (!shoot)
            {
               
                m_newRot = m_cageDir;
                
            }
            else
            {
                 m_newRot = m_target.position - m_transform.position;
            }
            m_newRot.y = 0;
            if (m_newRot.magnitude < 1)
            {
                shoot = true;
            }
            
           


            m_rotQuaternion = Quaternion.LookRotation(m_newRot);
            m_transform.rotation = Quaternion.Slerp(m_transform.rotation, m_rotQuaternion, m_rotLerp );

        }

        protected override void Move()
        {
            base.Move();

            if (shoot && m_cageDir.magnitude > m_minDirectionToShoot + 1)
            {
                shoot = false;
            }

            m_transform.position += m_transform.forward * m_baseSpeed *m_speedCoef * Time.deltaTime;
        }

        #endregion

        #region Private Function

        private void GetDirection()
        {
            m_dir = m_cage.position - m_target.position;
            m_dir.y = 0;
            m_targetPoint = m_target.position - m_dir.normalized * m_minDirectionToShoot ;
        }

        #endregion

    }
}
