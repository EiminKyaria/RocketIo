using System.Collections;
using System.Runtime.Remoting.Messaging;
using Com.Eimin.Personnal.Scripts.Game.ScriptableObjects;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers
{
    /// <summary>
    /// author : Matthias de Toffoli
    /// Manage the different state of the game
    /// </summary>
    public class GameManager : BaseManager<GameManager>
    {
        #region Inspector Variable

        [Header("SpawnParam")] [SerializeField]
        private SpawnParamSO m_spawnParam;

        [Header("Level Param")]

        [SerializeField]
        private string m_levelParamPath;
        [SerializeField]
        private string m_levelParamName;
        [SerializeField]
        private LevelParamSO m_levelParam;

        [SerializeField]
        private int m_LevelNumber = 1;

        [SerializeField]
        private string m_stageName;

        [SerializeField]
        private float m_maxPoint = 300;
        [SerializeField]
        private int m_GainPoint = 50;

        #endregion

        #region Private Variable

        private bool m_isFirstLaunch = false;

        private bool m_isNewMatch = false;

        private WaitForSeconds m_timeToWait;

        private int m_stageNumber;
        private float m_foeDifficulty;

        private int m_winStreak = 0;

        #endregion

        #region Getter

        public SpawnParamSO SpawnParam
        {
            get { return m_spawnParam; }
        }

        public int Gain
        {
            get { return m_GainPoint; }
        }

        public LevelParamSO LevelParam
        {
            get { return m_levelParam; }
        }

        public bool IsNewMatch
        {
            get { return m_isNewMatch; }
        }

        #endregion

        #region Events

        public GameStateEvent gameEvent;

        #endregion

        #region Enum var

        /// <summary>
        /// the current state of the game
        /// </summary>
        protected GameState state;

        /// <summary>
        /// state saved at start of pause
        /// </summary>
        protected GameState stateInPause;

        #endregion

        #region Boolean var

        /// <summary>
        /// said if the state is InGame
        /// </summary>
        public bool isInGame
        {
            get { return state == GameState.inGame; }
        }

        /// <summary>
        /// said if the state before pose is InGame
        /// </summary>
        public bool beforePauseIsInGame
        {
            get { return stateInPause == GameState.inGame; }
        }

        /// <summary>
        /// said if the state is GameWin
        /// </summary>
        public bool isWin
        {
            get { return state == GameState.gameWin; }
        }

        /// <summary>
        /// said if the state is Loos
        /// </summary>
        public bool isLoss
        {
            get { return state == GameState.gameOver; }
        }

        /// <summary>
        /// said if the state is Pause
        /// </summary>
        public bool isPause
        {
            get { return state == GameState.mainMenu; }
        }

        /// <summary>
        /// said if the state is Restart
        /// </summary>
        public bool isRestart
        {
            get { return state == GameState.restart; }
        }

        /// <summary>
        /// said if the state is FakeLoad
        /// </summary>
        public bool isFakeLoad
        {
            get { return state == GameState.fakeload; }
        }

        /// <summary>
        /// said if the state is InterRound
        /// </summary>
        public bool isInterRound
        {
            get { return state == GameState.interRound; }
        }

        #endregion

        #region Setter

        /// <summary>
        /// game take the state InGame
        /// </summary>
        public void setInGame()
        {
            setState(GameState.inGame);
        }

        /// <summary>
        /// game take the state Menu
        /// </summary>
        public void setMainMenu()
        {
            setState(GameState.mainMenu);
        }

        /// <summary>
        /// game take the state Menu
        /// </summary>
        public void setPausMenu()
        {
            stateInPause = state;
            setState(GameState.pauseMenu);
        }

        /// <summary>
        /// game take the state Win
        /// </summary>
        public void setWin()
        {
            setState(GameState.gameWin);
        }

        /// <summary>
        /// game take the state Loos
        /// </summary>
        public void setLoss()
        {
            setState(GameState.gameOver);
        }

        /// <summary>
        /// game take the state Restart
        /// </summary>
        public void setRestart()
        {
            setState(GameState.restart);

            StartCoroutine(RestartInterRound());
        }

        /// <summary>
        /// game take the state Fakeload
        /// </summary>
        public void setFakeLoad()
        {
            setState(GameState.fakeload);
        }

        /// <summary>
        /// game take the state Restart
        /// </summary>
        public void setInterRound()
        {
            setState(GameState.interRound);
        }

        /// <summary>
        /// game take the state Init
        /// </summary>
        public void setInit()
        {
            setState(GameState.init);
        }

        /// <summary>
        /// game take the state before pause
        /// </summary>
        public void setResum()
        {
            setState(stateInPause);
        }

        protected void setState(GameState pState)
        {
            state = pState;
            gameEvent.Invoke(state);
        }

        #endregion

        #region Monobehaviour's functions

        protected override void Awake()
        {
            base.Awake();

            gameEvent = new GameStateEvent();
            m_timeToWait = new WaitForSeconds(0.3f);
        }

        protected override void Start()
        {
            base.Start();
            SceneManager.sceneLoaded += OnSceneLoaded;

            StartCoroutine(StartCorou());
        }

        #endregion

        #region Coroutine

        IEnumerator StartCorou()
        {
            SetLevelNumber();

            LoadLevelParam();

            SetFoeDifficulty();

            SetStageNumber();

            while (!CheckAllStart())
            {
                yield return null;
            }

            setInit();

            while (!CheckAllInit())
            {
                yield return null;
            }


            m_isFirstLaunch = true;
            m_isNewMatch = true;

            LoadLevel();
        }

        private IEnumerator RestartFakeload()
        {
            yield return m_timeToWait;
            setFakeLoad();
        }

        private IEnumerator RestartInterRound()
        {
            yield return m_timeToWait;
            LoadLevel();
        }
        #endregion

        #region Private Function

        /// <summary>
        /// Check All the manager that need to listen the Game Manager
        /// </summary>
        /// <returns>if all the manager Listen the Game Manager</returns>
        private bool CheckAllStart()
        {
            return UIManager.instance.IsStart &&
                   HUDManager.instance.IsStart;

            //add all the manager that need to listen the Game Manager
        }

        /// <summary>
        /// Check all the manager that need to be init
        /// </summary>
        /// <returns> if all the manager that need to be init are init</returns>
        private bool CheckAllInit()
        {
            return UIManager.instance.IsInit &&
                   HUDManager.instance.IsInit;

            //add all the manager that need do be initializa to start the game
        }


        private void LoadLevel()
        {
            SceneManager.LoadScene( m_stageName + m_stageNumber);
        }

        private void OnSceneLoaded(Scene pScene, LoadSceneMode pMode)
        {
            if (m_isFirstLaunch)
            {
                setMainMenu();
                m_isFirstLaunch = false;
                return;
            }

            StartCoroutine(RestartFakeload());
        }

      

        private void LoadLevelParam()
        {

            m_levelParam = (LevelParamSO) Resources.Load(m_levelParamPath + m_levelParamName + m_LevelNumber);

            if (m_levelParam == null)
            {
                m_LevelNumber--;
                m_levelParam = (LevelParamSO)Resources.Load(m_levelParamPath + m_levelParamName + m_LevelNumber);
            }


        }

        private void SetLevelNumber()
        {
            if (PlayerPrefs.HasKey((SaveKey.LevelNumber.ToString())))
            {
                m_LevelNumber = PlayerPrefs.GetInt(SaveKey.LevelNumber.ToString());
                return;
            }

            m_LevelNumber = 0;

            PlayerPrefs.SetInt(SaveKey.LevelNumber.ToString(), m_LevelNumber);
        }

        private void SetStageNumber()
        {
            if(PlayerPrefs.HasKey(SaveKey.StageNumber.ToString()))
            {
                m_stageNumber = PlayerPrefs.GetInt(SaveKey.StageNumber.ToString());
                return;
            }

            SaveStageNumber();

        }

        private void SetFoeDifficulty()
        {
            if (PlayerPrefs.HasKey(SaveKey.FoeDifficulty.ToString()))
            {
                m_foeDifficulty = PlayerPrefs.GetFloat(SaveKey.FoeDifficulty.ToString());
                return;          
            }

           SaveFoeDifficulty();
        }

        private void SaveStageNumber()
        {

            m_stageNumber = m_levelParam.GetStageNumberParam();

            PlayerPrefs.SetInt(SaveKey.StageNumber.ToString(), m_stageNumber);
        }

        private void SaveFoeDifficulty()
        {

            m_foeDifficulty = m_levelParam.GetDifficultyPercent();

            PlayerPrefs.SetFloat(SaveKey.FoeDifficulty.ToString(), m_foeDifficulty);
        }

        #endregion

        #region Public Function

        public void CheckMatchEnd(int pPlayerScore, int pEnemyScore)
        {
            if (pPlayerScore >= m_maxPoint)
            {
                m_isNewMatch = true;
                setWin();

                m_winStreak++;

                if (m_winStreak >= m_levelParam.VictoryNumber)
                {
                    m_LevelNumber++;
                    LoadLevelParam();
                }

                SaveStageNumber();

                SaveFoeDifficulty();

                return;
            }

            if (pEnemyScore >= m_maxPoint)
            {
                m_isNewMatch = true;
                setLoss();
                return;
            }

            m_isNewMatch = false;
            setInterRound();

        }

        #endregion

        #region Listener

        #endregion
    }
}