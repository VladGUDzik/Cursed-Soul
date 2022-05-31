using System;
using Hero.HeroState.SubStates;
using ScriptbleObjects.Weapons;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected SO_WeaponData weaponData;
        [SerializeField] private float comboDelay;
        private Animator _baseAnimator;
        protected Animator WeaponAnimator;

        protected HeroAttackState AttackState;

        protected Core.Core Core;

        protected int AttackCounter;
        private float _startComboDelay;

        protected virtual void Awake()
        {
            _baseAnimator = transform.Find("Base").GetComponent<Animator>();
            WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

            gameObject.SetActive(false);
        }

        public virtual void EnterWeapon()
        {
            gameObject.SetActive(true);

            if (AttackCounter >= weaponData.amountOfAttacks)
            {
                AttackCounter = 0;
            }
            if (Time.time >= _startComboDelay + comboDelay)
            {
                AttackCounter=0;
            }

            _baseAnimator.SetBool("attack", true);
            WeaponAnimator.SetBool("attack", true);

            _baseAnimator.SetInteger("attackCounter", AttackCounter);
            WeaponAnimator.SetInteger("attackCounter", AttackCounter);
        }

        public virtual void ExitWeapon()
        {
            _baseAnimator.SetBool("attack", false);
            WeaponAnimator.SetBool("attack", false);
            
            _startComboDelay = Time.time;
            
             AttackCounter++;

            gameObject.SetActive(false);
        }

        #region Animation Trigger

        public virtual void AnimationFinishTrigger()
        {
            AttackState.AnimationFinishTrigger();
        }

        public virtual void AnimationStartMovementTrigger()
        {
            AttackState.SetHeroVelocity(weaponData.movementSpeed[AttackCounter]);
        }

        public virtual void AnimationStopMovementTrigger()
        {
            AttackState.SetHeroVelocity(0f);
        }

        public virtual void AnimationTurnOffFlipTrigger()
        {
            AttackState.SetFlipCheck(false);
        }

        public virtual void AnimationTurnOnFlipTrigger()
        {
            AttackState.SetFlipCheck(true);
        }

        public virtual void AnimationActionTrigger()
        {
        }

        #endregion

        public void InitializeWeapon(HeroAttackState state, Core.Core core)
        {
            AttackState = state;
            Core = core;
        }
    }
}