using System;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using Com.Eimin.Personnal.Scripts.Utils.Interface;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers
{

    /// <summary>
    /// This Class is the Screen Information needed to order screen And Target them 
    /// </summary>
    [Serializable]
    public class ScreenInfo
    {
        public string name;
        public IScreen screen;
        public GameObject screenInstance;

        public IScreen GetScreenComponent()
        {
            return screenInstance.GetComponent<IScreen>();
        }
    }

    public class UIManager : BaseManager<UIManager>
    {
        #region Inspector Variable

        [Header("screen list")]
        public List<ScreenInfo> _screenOrder;

        #endregion

        #region Private Variable

        private ScreenInfo _currentScreen;
        private ScreenInfo _previewScreen;

        #endregion


        #region Monobehaviour Functions

        protected override void Awake()
        {
            base.Awake();

            InitScreenOrder();
           // CLoseAllScreen();
        }

        protected override  void Start()
        {
            base.Start();
            CLoseAllScreen();
            GameManager.instance.gameEvent.AddListener(ListenerGameState);
            BlackScreen();

            m_isStart = true;
        }

        #endregion

        #region Screen Navigation function's

        /// <summary>
        /// Allow to Open a screen  by giving the screen information
        /// </summary>
        /// <param name="selectedScreen"> the screen to open</param>
        public void OpenScreen (ScreenInfo selectedScreen)
        {
            if (_currentScreen != null)
            {  
                _currentScreen.screen.Close();
                _previewScreen = _currentScreen;
            }
            selectedScreen.screen.Open();
            _currentScreen = selectedScreen;
        }

        /// <summary>
        /// Allow to have the screen's information by giving the Screen name
        /// </summary>
        /// <param name="pScreenName"> The Screen's name </param>
        /// <returns></returns>
        public ScreenInfo GetScreen(string pScreenName)
        {
            ScreenInfo screen;
            foreach(ScreenInfo cScreen in _screenOrder)
            {
                if (cScreen.name == pScreenName)
                {
                    screen = cScreen;
                    return screen;
                }
            }
            Debug.LogError("Auncun Screen ne Correspond à votre demande");
            return null;
        }

        /// <summary>
        /// Allow to open the Next screen in the Screen Order's List
        /// </summary>
        public void OpenNextScreen()
        {
            int nextScreenIndex = _screenOrder.IndexOf(_currentScreen) + 1;
            if(nextScreenIndex >= _screenOrder.Count)
            {
                _currentScreen.screen.Close();
                if(DebugMode) Debug.LogError("il n'y a pas de screen suivant");
                return;
            }
            OpenScreen(GetScreen(_screenOrder[nextScreenIndex].name));
        }

        /// <summary>
        /// allow to open the preview screen in the Screen Order List
        /// </summary>
        public void OpenPreviewScreen()
        {
            int nextScreenIndex = _screenOrder.IndexOf(_currentScreen) - 1;
            if (nextScreenIndex < 0)
            {
                _currentScreen.screen.Close();
               if(DebugMode) Debug.LogError("il n'y a pas de screen precedent");
                return;
            }
            OpenScreen(GetScreen(_screenOrder[nextScreenIndex].name));
        }

        /// <summary>
        /// Close All openScreen()
        /// </summary>
        public void CLoseAllScreen()
        {
            foreach(ScreenInfo cScreen in _screenOrder)
            {
                cScreen.screen.Close();//a remplacer pas cScreen.screen.setActive(False)
            } 
        }

        /// <summary>
        /// Permet d'initialiser les screen dans screen Order
        /// </summary>
        private void InitScreenOrder()
        {
            IScreen screen;
            int length = _screenOrder.Count;
            ScreenInfo cScreen;
            for (int i = length - 1; i >= 0; i--)
            {
                cScreen = _screenOrder[i]; 
                screen = cScreen.GetScreenComponent();
               
                if (screen == null)
                {
                    if(DebugMode) Debug.LogError(cScreen.screenInstance.name + " n'a pas de composant héritant de Screen");
                    _screenOrder.Remove(cScreen);
                    continue;
                }
                cScreen.screen = screen;
            }
        }

        /// <summary>
        /// Allow to open the last screen opened
        /// </summary>
        public void Back()
        {
            if (_currentScreen != null) _currentScreen.screen.Close();
            if (_previewScreen != null) OpenScreen(_previewScreen);
        }

        #endregion

        #region Close Game Function

        /// <summary>
        /// Allow to close the application
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }

        #endregion

        #region Callback Functions

        protected override void ListenerGameState(GameState pState)
        {
            base.ListenerGameState(pState);
            switch (pState)
            {
                case GameState.init:
                    m_isInit = true;
                    break;
                case GameState.mainMenu:
                    Menu();
                    break;
                case GameState.inGame:
                    CLoseAllScreen();
                    break;
                case GameState.fakeload:
                    CLoseAllScreen();
                    break;
                case GameState.pauseMenu:
                    break;
                case GameState.interRound:
                    InterRound();
                    break;
                case GameState.gameWin:
                    Win();
                    break;
                case GameState.gameOver:
                    GameOver();
                    break;
                case GameState.restart:
                    BlackScreen();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("pState", pState, null);
            }
        }

        /// <summary>
        /// the call back function to the onMenu event of the game manager
        /// </summary>
        protected void Menu()
        {
            OpenScreen(_screenOrder[0]);
        }

        protected void BlackScreen()
        {
            OpenScreen(GetScreen("Black_Screen"));
        }

        protected void InterRound()
        {
            OpenScreen(GetScreen("InterRound_Screen"));
        }

        protected void Win()
        {
            OpenScreen(GetScreen("Win_Screen"));
        }

        protected void GameOver()
        {
            OpenScreen(GetScreen("GameOver_Screen"));
        }

        #endregion

    }
}