using System;
using Com.Eimin.Personnal.Scripts.Utils.Classes;

namespace Com.Eimin.Personnal.Scripts.Game.Monobehaviours
{
    public class MonoBehaviourStateMachine : MonoBehaviourLegacy
    {
        #region Properties
        protected Action doAction;
        protected ObjectState m_state;

        public ObjectState State
        {
            get { return m_state; }
        }

        #endregion

        #region MonobeHaviour's functions
        protected override void Start()
        {
            base.Start();
            SetModeNormal();
        }
        #endregion

        #region Action Void
        protected virtual void SetModeVoid()
        {
            doAction = DoActionVoid;
        }

        protected virtual void DoActionVoid()
        {

        }
        #endregion

        #region Action Normal
        protected virtual void SetModeNormal()
        {
            doAction = DoActionNormal;
        }

        protected virtual void DoActionNormal()
        {

        }
        #endregion
    }
}
