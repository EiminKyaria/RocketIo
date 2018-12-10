using System.Collections;
using Com.Eimin.Personnal.Scripts.Managers.VirtualsManagers;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours.UI.Screens
{
    /// <summary>
    /// 
    /// </summary>
    public class InterRound : Screen<InterRound>
    {
        [SerializeField]
        private float m_endTime = 1f;

        [SerializeField]
        private Text m_goalText;
        [SerializeField]
        private float m_maxTxtScale = 2;

        [Header("Score Display")] [SerializeField]
        private Text m_playerScoreText;
        [SerializeField]
        private Text m_enemyScoreText;

        [SerializeField] private AnimationCurve m_scoreAnim;

        private string m_string = "";

        private int m_playerScr = 0;
        private int m_enemyScr = 0;

        private int m_actualPlayerScore = 0;
        private int m_actualEnemyScore = 0;

        private Color m_stockedColor;


        public override void Open()
        {
            base.Open();

            m_actualPlayerScore = HUDManager.instance.PlayerScore;
            m_actualEnemyScore = HUDManager.instance.EnemyScore;

            ResetScore();

            SetGoalColor();

            StartCoroutine(ScoreTime());
        }

        public override void Close()
        {
            Time.timeScale = 1;
            base.Close();
        }

        private IEnumerator ScoreTime()
        {
            float elapseTime = 0, maxtime = 0.75f * m_endTime, Score1 = 0, Score2 = 0;

            while (elapseTime < maxtime)
            {
                Score1 = (int) (m_playerScr +
                                ((m_actualPlayerScore - m_playerScr) * m_scoreAnim.Evaluate(elapseTime / maxtime)));
                Score2 = (int) (m_enemyScr +
                                ((m_actualEnemyScore - m_enemyScr) * m_scoreAnim.Evaluate(elapseTime / maxtime)));

                UpdateText(Score1, Score2 , (elapseTime / maxtime));

                Time.timeScale = 1 - (elapseTime / m_endTime);

                yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
                elapseTime += Time.unscaledDeltaTime;
            }

            yield return new WaitForSecondsRealtime(m_endTime - maxtime);

            m_enemyScr = m_actualEnemyScore;
            m_playerScr = m_actualPlayerScore;

            GameManager.instance.setRestart();
        }
    
        private void UpdateText(float pScore1 = 0, float pScore2 = 0, float pPercent = 0)
        {
            m_playerScoreText.text = pScore1 + m_string;
            m_enemyScoreText.text = pScore2 + m_string;

            m_goalText.transform.localScale = new Vector3(m_maxTxtScale *pPercent, m_maxTxtScale * pPercent);

            m_stockedColor = m_goalText.color;
            m_stockedColor.a = pPercent;
            m_goalText.color = m_stockedColor;

        }

        private void ResetScore()
        {
            if (m_actualPlayerScore == 0)
            {
                m_playerScr = m_actualPlayerScore;
            }

            if (m_actualEnemyScore == 0)
            {
                m_enemyScr = m_actualEnemyScore;
            }
        }

        private void SetGoalColor()
        {
            m_goalText.transform.localScale = new Vector3(1, 1);

            if (m_playerScr != m_actualPlayerScore)
            {
                m_goalText.color = Color.blue;
            }

            if (m_enemyScr != m_actualEnemyScore)
            {
                m_goalText.color = Color.red;
            }
        }
    }
}