using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.ScriptableObjects
{

    [CreateAssetMenu(fileName = "ExempleFileName", menuName = "ExempleMenuName", order = 1)]
    public class ExempleSO : ScriptableObject
    {
        public string ExempleSOName = "";
    }
}