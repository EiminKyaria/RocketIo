using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers
{

    /// <summary>
    /// 
    /// </summary>
    public class HUDManager : BaseManager<HUDManager>
    {
       
        #region Editor Variables

        [SerializeField]
        private ScreenInfo m_HUDScreen;

        [SerializeField]
        private Text m_score;

        [SerializeField]
        private Text m_TimerText;

        #endregion

        #region Private Variable

        private int m_playerScore = 0;
        private int m_enemyScore = 0;

        private float m_timer = 3;

        private string m_WaitText = "Waiting For \n Opponent";
        private string m_dotText = "";

        #endregion

        #region Getter / Setter

        public int PlayerScore
        {
            get { return m_playerScore; }
            set { m_playerScore = value; }
        }

        public int EnemyScore
        {
            get { return m_enemyScore; }
            set { m_enemyScore = value; }
        }

        #endregion

        #region MonoBehaviour Functions

        protected override void Start()
        {
            base.Start();

            GameManager.instance.gameEvent.AddListener(ListenerGameState);

            m_isStart = true;

        }

        #endregion

        #region Public Function

        public void UpdateScore()
        {
            m_score.text = m_playerScore + " : " + m_enemyScore;
        }

        #endregion

        #region private Functions

        private void InitHUD()
        {
            m_HUDScreen.screen = m_HUDScreen.GetScreenComponent();
            if (m_HUDScreen.screen == null)
            {
                if (DebugMode) Debug.LogError(m_HUDScreen.screenInstance.name + " n'a pas de composant héritant de Screen");
                return;
            }
            CloseHUD();
        }

        private IEnumerator Timer()
        {
            int dotNumber = 0;
            m_TimerText.gameObject.SetActive(true);
            m_score.gameObject.SetActive(false);

            if (GameManager.instance.IsNewMatch)
            {
                m_timer = Random.Range(2, 5);
                m_dotText = ".";

                while (m_timer > 0)
                {
                    m_dotText += ".";
                    dotNumber++;
                    if (dotNumber % 3 == 0)
                    {
                        m_dotText = ".";
                    }
                    m_TimerText.text = m_WaitText + m_dotText;
                    yield return new WaitForSeconds(0.5f);
                    m_timer -= 0.5f;
                }
            }
            else
            {
                m_timer = 3;
                while (m_timer > 0)
                {
                  
                    m_TimerText.text = m_timer+"";
                    yield return new WaitForSeconds(1f);
                    m_timer--;
                }
            }
          

            m_TimerText.text = "Start !";
            yield return new WaitForSeconds(0.5f);
            m_TimerText.gameObject.SetActive(false);
            m_score.gameObject.SetActive(true);
            GameManager.instance.setInGame();


        }


        private void ResetScore()
        {
            m_playerScore = 0;
            m_enemyScore = 0;
        }
        #endregion

        #region CallBack Functions

        protected override void ListenerGameState(GameState pState)
        {
            base.ListenerGameState(pState);
            switch (pState)
            {
                case GameState.init:
                    InitHUD();
                    m_isInit = true;
                    break;
                case GameState.mainMenu:
                    CloseHUD();
                    break;
                case GameState.inGame:
                    OpenHUD();
                    break;
                case GameState.fakeload:
                    OpenHUD();
                    StartCoroutine(Timer());
                    break;
                case GameState.pauseMenu:
                    CloseHUD();
                    break;
                case GameState.interRound:
                    CloseHUD();
                    break;
                case GameState.gameWin:
                    CloseHUD();
                    break;
                case GameState.gameOver:
                    CloseHUD();
                    break;
                case GameState.restart:
                    if (GameManager.instance.IsNewMatch)
                    {
                        ResetScore();
                        UpdateScore();
                    }
                    CloseHUD();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("pState", pState, null);
            }

        }

        private void OpenHUD()
        {
            m_HUDScreen.screen.Open();
        }

        private void CloseHUD()
        {
            m_HUDScreen.screen.Close();
        }

        #endregion


    }
}