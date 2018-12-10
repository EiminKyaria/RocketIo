using System;
using System.Collections;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Utils.Tools;
using Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Com.Eimin.Personnal.Scripts.Managers.ObjectsManagers
{
    public class EffectManager : PhysicsManager<EffectManager, Effect>
    {
        #region Private Functions

        private Camera m_cameraToShake;

        #endregion

        #region MonoBehaviour's functions

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            GameManager.instance.gameEvent.AddListener(ListenerGameState);

            m_cameraToShake = Camera.main;

            m_isStart = true;
        }

        #endregion

        #region Public Functions

        public void Shake()
        {
            StartCoroutine(ShakeCam());
        }

        #endregion

        #region Coroutine

        private IEnumerator ShakeCam(float pDuration = 0.3f, float magnitude = 0.1f)
        {
            float x, y, ElapseTime = 0, Duration = pDuration;

            Transform CamTranform = Camera.main.transform;

            Vector3 OriginalPos = CamTranform.localPosition;
            
            while (ElapseTime < Duration)
            {
                x = Random.Range(-1f, 1f) * magnitude;
                y = Random.Range(-1f, 1f) * magnitude;

                CamTranform.localPosition = new Vector3( x, y, OriginalPos.z);

                ElapseTime += Time.deltaTime;

                yield return null;
            }

            CamTranform.localPosition = OriginalPos;
        }

        private IEnumerator FreezeTIme(float pDuration = 0.2f)
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(pDuration);
            Time.timeScale = 1f;
        }

        #endregion

        #region Listener

        protected override void ListenerGameState(GameState pState)
        {
            base.ListenerGameState(pState);
            switch (pState)
            {
                case GameState.init:
                    m_isInit = true;
                    break;
                case GameState.mainMenu:
                    break;
                case GameState.inGame:
                    break;
                case GameState.fakeload:
                    break;
                case GameState.pauseMenu:
                    break;
                case GameState.interRound:
                    break;
                case GameState.gameWin:
                    break;
                case GameState.gameOver:
                    break;
                case GameState.restart:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("pState", pState, null);
            }
        }

        #endregion
    }
}