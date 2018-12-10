using Com.Eimin.Personnal.Scripts.Utils.Interface;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours.UI.Screens
{

    /// <summary>
    /// 
    /// </summary>
    public class Screen<T> : MonoBehaviorLegacySingleton<T>,IScreen where T : MonoBehaviour
    {
        /// <summary>
        /// Allow to open a screen
        /// </summary>
        public virtual void Open() {
            gameObject.SetActive(true);
        }
        /// <summary>
        /// Allow to close a screen
        /// </summary>
        public virtual void Close() {
            gameObject.SetActive(false);
        }
            
    }
}