using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;
using Weapons;

namespace Hero.HeroState.SubStates
{
    public class HeroAttackState : HeroAbiilityState
    {
        private Weapon _weapon;

        private int _xInput;
    
        private float _velocityToSet;
    
        private bool _setVelocity;
        private bool _shouldCheckFlip;
        public HeroAttackState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) 
            : base(hero, stateMachine, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _setVelocity = false;
        
            _weapon.EnterWeapon();
        }

        public override void Exit()
        {
            base.Exit();
        
            _weapon.ExitWeapon();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _xInput = hero.inputHandler.normInputX;

            if (_shouldCheckFlip)
            {
                Movement?.CheckIfShouldFlip(_xInput);
            }

            if (_setVelocity)
            {
                Movement?.SetVelocityX(_velocityToSet * Movement.facingDirection);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
            _weapon.InitializeWeapon(this,core);
        }

        public void SetHeroVelocity(float velocity)
        {
            Movement?.SetVelocityX(velocity * Movement.facingDirection);

            _velocityToSet = velocity;
            _setVelocity = true;
        }

        public void SetFlipCheck(bool value)
        {
            _shouldCheckFlip = value;
        }

        #region Animation Trigger

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();

            IsAbilityDone = true;
        }
      

        #endregion
    }
}
