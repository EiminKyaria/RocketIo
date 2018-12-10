using System.Collections;
using System.Collections.Generic;
using Com.Eimin.Personnal.Scripts.Game.Monobehaviours;
using Com.Eimin.Personnal.Scripts.Utils.Classes;
using UnityEngine;

namespace Com.Eimin.Personnal.Scripts.Utils.Tools
{
    public class LifeSystem : MonoBehaviourLegacy
    {
        #region Inspector Variable

        [Header("Life Param")]

        [SerializeField]
        private int _currentLife = 10;
        [SerializeField]
        private int _maxLife = 10;
        [SerializeField]
        private int _armor = 2;

        #endregion

        #region Private Variable


        private TouchableState _state;
    

        #endregion

        #region Additionnal Variable

      
        #endregion

        #region Getter/setter

        public int CurrentLife
        {
            get { return _currentLife; }
        }

        public int MaxLife
        {
            get { return _maxLife; }
        }

        public int Armor
        {
            get { return _armor; }
        }

        public TouchableState state
        {
            get { return _state; }
        }
       
        #endregion

        #region Events

        public BaseGameEvent isDead;
        public GaugeEvent isHit;
        public GaugeEvent isHeal;
   

        #endregion

        #region MonoBehaviour Functions

        protected void Awake()
        {
            base.Awake();

            isDead = new BaseGameEvent();
            isHit = new GaugeEvent();
            isHeal = new GaugeEvent();

        }

        #endregion

        #region Private Functions

        /// <summary>
        /// apply the damage to the game object wow belowed the script
        /// </summary>
        /// <param name="pDamage"> the amount of damage taken</param>
        protected virtual void Gethit(int pDamage)
        {
            if (pDamage <= 0)
            {
                return;
            }
            _currentLife -= pDamage;
            if (_currentLife <= 0)
            {
                if(DebugMode) Debug.Log(name + ": death");


                isDead.Invoke();
              

                return;
            }
    
            isHit.Invoke(_currentLife, _maxLife);

        }

        #endregion

        #region Public functions

        /// <summary>
        /// Allow to heal the belower of this script with a amount oh HP
        /// </summary>
        /// <param name="pHealPoint"> the amount of HP to Heal</param>
        public void Heal(int pHealPoint)
        {
            if (pHealPoint <= 0)
            {
                return;
            }
            _currentLife += pHealPoint;
            if (_currentLife > _maxLife)
            {
                _currentLife = _maxLife;
            }
            isHeal.Invoke(_currentLife, _maxLife);
        }

        /// <summary>
        /// test and  prepare the amount of damage
        /// </summary>
        /// <param name="pAttackValue"> the int value of the attack </param>
        public void TakeDammage(int pAttackValue)
        {
            if (_state == TouchableState.god) return;
            if (_currentLife <= 0) return;

            int damage = pAttackValue - _armor;

            if (damage < 0) damage = 0;
            Gethit(damage);

        }

        public void TakeDamageWithPercent(int pPercent)
        {

            float cPercent = Mathf.Clamp(pPercent, 0, 100);
            cPercent *= 0.01f;

            TakeDammage((int)(cPercent * _maxLife));

        }
       
        /// <summary>
        /// allow to change the state of the object
        /// </summary>
        /// <param name="pState"> the next state of the object </param>
        public void SetTouchableState(TouchableState pState)
        {
            _state = pState;
        }

        /// <summary>
        /// allow to initialise the object caracteristics
        /// </summary>
        /// <param name="pPV"> current HP </param>
        /// <param name="pMaxPV"> current max  HP </param>
        /// <param name="pArmor"> the current Ar mor</param>
        /// <param name="pState"> the initialise state </param>
        
        public void SetCarac(int pPV, int pMaxPV, int pArmor = 0, TouchableState pState = TouchableState.normal)
        {

            _currentLife = pPV;
            _maxLife = pMaxPV;
            _armor = pArmor;
            _state = pState;
          
        }

        #endregion

        #region Additional Functions

       

        #endregion
    }
}
