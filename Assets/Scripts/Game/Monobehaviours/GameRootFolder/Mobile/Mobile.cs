using System.Collections;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours
{
    public abstract class Mobile : MonoBehaviourStateMachine
    {

        #region Editor Variable

        [Header("Gaphics")]
        [SerializeField]
        protected GameObject m_graphic;

        [Header("Mobile param")]

        [SerializeField]
        protected float m_baseSpeed = 10f;

        [SerializeField]
        protected float m_impactForce = 50f;

        [SerializeField]
        protected MobileType m_type;
       


        #endregion

        #region Protected Variables

        protected Quaternion m_rotQuaternion;

        protected float m_speedCoef = 0f;


        #endregion


        #region MonoBehaviour's functions
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
           SetModeNormal();
        }

        protected override void Update()
        {
            base.Update();

            doAction();


        }

        protected virtual void OnCollisionEnter(Collision col)
        {

            if (!col.collider.CompareTag("Balloon")) return;

            Balloon c_balloon = col.gameObject.GetComponent<Balloon>();
            c_balloon.OnCollision(m_transform.forward * m_speedCoef * m_impactForce + m_transform.up *m_speedCoef*(m_impactForce/3), m_type);

        }


        protected virtual void Move()
        {
        }

        protected virtual void GetRotation()
        {
           

        }

        #endregion

        #region Death Action

        

        #endregion
    }
}
