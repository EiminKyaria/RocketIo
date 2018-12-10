using System;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using Com.Eimin.Personnal.Scripts.Utils.Interface;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers
{

    public class InputManager : BaseManager<InputManager>
    {
        #region Private Variable

        private bool m_isClick = false;

        private Vector3 m_mouseStartPosition;
        private Vector3 m_mouseEndPosition;

        private Vector3 m_inputs;

        #endregion

        #region Inspector Variable

        [SerializeField] private float m_maxJoyStick = 0.3f;
     
        #endregion

        #region Getter/Setter

        public Vector3 Inputs
        {
            get { return m_inputs; }
        }

        public bool IsTouch
        {
            get { return m_isClick; }
        }



        #endregion

        #region Events

        public BaseGameEvent ClickBegin;
        public BaseGameEvent ClickEnd;

        #endregion

        #region Monobehaviour Functions

        protected override void Awake()
        {
            base.Awake();

            ClickBegin = new BaseGameEvent();
            ClickEnd = new BaseGameEvent();
        }
        protected override void Start()
        {
            base.Start();
            
            GameManager.instance.gameEvent.AddListener(ListenerGameState);

            m_isStart = true;
        }

        protected void Update()
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        if (Input.touchCount > 0 && !m_isClick)
        {
             ClickBegin.Invoke();
             m_isClick = true;
        }

        if (Input.touchCount <= 0)
        {
            ClickEnd.Invoke();
            m_mouseStartPosition = Vector3.zero;
            m_inputs = Vector3.zero;
            m_isClick = false;
        }
#endif

#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR || UNITY_WEBPLAYER
            if (Input.GetMouseButton(0) && !m_isClick)
            {
                ClickBegin.Invoke();
                m_isClick = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                ClickEnd.Invoke();
                m_mouseStartPosition = Vector3.zero;
                m_inputs = Vector3.zero;
                m_isClick = false;
            }


#endif
            InputUpdate();

        
        }

        #endregion

        #region Private Functions

        private void InputUpdate()
        {

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    m_mouseStartPosition = Input.GetTouch(0).position;
                }
                else if (m_mouseStartPosition != Vector3.zero)
                {
                    m_mouseEndPosition = Input.GetTouch(0).position;
                    Vector3 _v3MouseDir = m_mouseEndPosition - m_mouseStartPosition;
                    _v3MouseDir = new Vector3(_v3MouseDir.x / Screen.width, _v3MouseDir.y / Screen.height, 0f);
                    m_inputs = new Vector3(_v3MouseDir.x / m_maxJoyStick , _v3MouseDir.y / m_maxJoyStick, 0);

                }
            }

#endif

#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR || UNITY_WEBPLAYER
            if (Input.GetMouseButtonDown(0))
            {
                m_mouseStartPosition = Input.mousePosition;
            }
            else if ( m_mouseStartPosition != Vector3.zero)
            {

                m_mouseEndPosition = Input.mousePosition;
                Vector3 _v3MouseDir = m_mouseEndPosition - m_mouseStartPosition;
                _v3MouseDir = new Vector3(_v3MouseDir.x / Screen.width, _v3MouseDir.y / Screen.height, 0f);
                m_inputs = new Vector3(_v3MouseDir.x / m_maxJoyStick, _v3MouseDir.y / m_maxJoyStick, 0);
                      
            }
#endif



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

        private void ResetValue()
        {
            m_isClick = false;
            m_mouseStartPosition = Vector3.zero;
            m_inputs = Vector3.zero;
             
        }

        #endregion

    }




}
