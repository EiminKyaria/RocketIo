using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.ScriptableObjects
{

    [CreateAssetMenu(fileName = "SpawnTemplate", menuName = "SpawnTemplateFile", order = 1)]
    public class SpawnParamSO : ScriptableObject
    {

        [Header("Player")]
        public GameObject PlayerTemplate;
    }
}