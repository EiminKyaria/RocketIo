using System.Collections;
using Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours.UI.Screens
{

    /// <summary>
    /// 
    /// </summary>
    public class BlackScreen : Screen<BlackScreen>
    {

        [SerializeField]
        private Image m_blackScreen;

        [SerializeField]
        private float m_fadeTime = 0.7f;

        private WaitForSeconds m_waitTime;
        private Coroutine fade;

        protected override void Awake()
        {
            base.Awake();
            m_blackScreen = GetComponent<Image>();
            m_waitTime = new WaitForSeconds(m_fadeTime);

        }
        
        public override void Open()
        {
            base.Open();
            m_blackScreen.CrossFadeAlpha(1f,m_fadeTime, false);
        }

        public override void Close()
        {
            m_blackScreen.CrossFadeAlpha(0.0f,m_fadeTime,false);
            if (gameObject.active)
            {
                fade = StartCoroutine(CloseCorou());
            }
           

        }

        private IEnumerator CloseCorou()
        {
            yield return m_waitTime;
            gameObject.SetActive(false);
            fade = null;
        }

        public void OnDisable()
        {
            if (fade != null)
            {
                StopCoroutine(fade);
                fade = null;
            }
        }

    }
}