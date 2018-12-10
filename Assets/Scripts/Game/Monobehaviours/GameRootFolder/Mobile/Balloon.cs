using System;
using System.Collections;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours
{
    public class Balloon : MonoBehaviourLegacy
    {

        #region Editor Variable

        [SerializeField]
        private GameObject m_graphic;

        [SerializeField]
        private TrailRenderer m_trail;

        #endregion

        #region Private Variable

        private Material m_material;
        private Rigidbody m_rb;

        #endregion

        #region MonoBehaviour's functions
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            m_material = m_graphic.GetComponent<Renderer>().material;
            m_rb = GetComponent<Rigidbody>();
        }

        protected override void Update()
        {
            base.Update();
        }

        #endregion

        #region Public function

        public void OnCollision(Vector3 pForceVector,MobileType ptype)
        {
            m_rb.velocity = Vector3.zero;
            m_rb.AddForce(pForceVector);

            switch (ptype)
            {
                case MobileType.Enemy:
                    m_material.color = Color.red;
                    m_trail.startColor = Color.red;
                    break;
                case MobileType.Player:
                    m_material.color = Color.blue;
                    m_trail.startColor= Color.blue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("ptype", ptype, null);
            }

        }

        public void OnGoal()
        {

        }


        #endregion
    }
}
