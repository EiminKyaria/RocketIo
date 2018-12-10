using System;
using System.Collections;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Managers.ObjectsManagers;
using Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours
{
    public class Goal : MonoBehaviourLegacy
    {

        #region Editor Variable

        [SerializeField]
        private MobileType m_cageType;

        [SerializeField]
        private List<Rigidbody> m_allRb;

        #endregion

        #region MonoBehaviour's functions
        // Use this for initialization
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected virtual void OnCollisionEnter(Collision col)
        {
            if (!GameManager.instance.isInGame) return;
            if (!col.collider.CompareTag("Balloon")) return;

            switch (m_cageType)
            {
                case MobileType.Enemy:
                    HUDManager.instance.PlayerScore+= GameManager.instance.Gain;
                    break;
                case MobileType.Player:
                    HUDManager.instance.EnemyScore += GameManager.instance.Gain;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            HUDManager.instance.UpdateScore();

            GameManager.instance.CheckMatchEnd(HUDManager.instance.PlayerScore, HUDManager.instance.EnemyScore);

            EffectManager.instance.Shake();

            Explosion();

        }

        private void Explosion()
        {
            foreach (Rigidbody c_rb in m_allRb)
            {
                c_rb.constraints = RigidbodyConstraints.None;
                c_rb.AddExplosionForce(1000,m_transform.position,50);
            }
        }

        #endregion
    }
}
