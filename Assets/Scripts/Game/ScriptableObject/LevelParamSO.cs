using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Game.Monobehaviours;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.ScriptableObjects
{

    [CreateAssetMenu(fileName = "Level_0", menuName = "LevelParam", order = 1)]
    public class LevelParamSO : ScriptableObject
    {
        public string ExempleSOName = "";

        [Header("Level Param")]

        public int VictoryNumber = 1;

        [Header("Enemy Param")]

        public Vector2 DifficultypercentPlage;

        [Header("Stage Param")]

        public List<int> StagesNumberParam;
        
        public float GetDifficultyPercent()
        {

            return Random.Range(DifficultypercentPlage.x, DifficultypercentPlage.y) / 100f;
           
        }

        public int GetStageNumberParam()
        {

            return StagesNumberParam[Random.Range(0, StagesNumberParam.Count)];

        }


    }
}