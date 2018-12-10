using UnityEngine.Events;

namespace Com.Eimin.Personnal.Scripts.Utils.Classes
{
    /// <summary>
    /// Utils used for all public unityEvent we need
    /// </summary>
    

    public class FloatEvent : UnityEvent<float> { }
    public class IntFloatEvent : UnityEvent<int,float> { }
    public class GameStateEvent : UnityEvent<GameState> { }
    public class BaseGameEvent : UnityEvent { }
    public class GaugeEvent : UnityEvent<float, float> { } 
}