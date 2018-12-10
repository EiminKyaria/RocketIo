using System.Collections;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours
{
    public class Player : Mobile
    {

        #region Editor Variable

        [SerializeField]
        private float m_deadZone = 0.2f;

        [SerializeField]
        private float m_rotLerpCoef = 20f;

        [SerializeField]
        private Transform m_target;

        [SerializeField]
        private Transform m_arrow;

        #endregion

        #region Private Variables

        private Vector3 m_newRot;

        private float m_InputMagne = 0f;

        private Vector3 m_targetDir;
        private Quaternion m_targetRot;
        #endregion

        #region MonoBehaviour's functions
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            m_newRot = new Vector3();
            m_targetDir = new Vector3();
            m_targetRot = new Quaternion();
            m_type = MobileType.Player;
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

            base.DoActionNormal();

            m_InputMagne = InputManager.instance.Inputs.magnitude;
            GetRotation();
            Move();
            LookTarget();
        }

        #endregion

        #region Overrided Function

        protected override void GetRotation()
        {
            base.GetRotation();

            if (m_InputMagne < m_deadZone) return;

            m_newRot.Set(
                InputManager.instance.Inputs.x,
                0,
                InputManager.instance.Inputs.y);

            m_rotQuaternion = Quaternion.LookRotation(m_newRot);
            m_transform.rotation = Quaternion.Slerp(m_transform.rotation, m_rotQuaternion, m_rotLerpCoef);
        }

        protected override void Move()
        {
            base.Move();

            if (m_InputMagne < m_deadZone) return;

            m_speedCoef = (m_InputMagne - m_deadZone) / (1 - m_deadZone);
            m_speedCoef = Mathf.Clamp01(m_speedCoef);

            m_transform.position += m_transform.forward * m_speedCoef * m_baseSpeed * Time.deltaTime;
        }


        private void LookTarget()
        {
            m_targetDir.Set(m_target.position.x-m_arrow.position.x,0,m_target.position.z-m_arrow.position.z);
            m_targetRot = Quaternion.LookRotation(m_targetDir);
            m_arrow.rotation = Quaternion.Slerp(m_arrow.rotation, m_targetRot, m_rotLerpCoef);


        }
        #endregion
    }
}
