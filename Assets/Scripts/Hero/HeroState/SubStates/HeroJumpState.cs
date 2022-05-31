using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;

namespace Hero.HeroState.SubStates
{
    public class HeroJumpState : HeroAbiilityState
    {
        private int _amountOfJumpsLeft;
        public HeroJumpState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) :
            base(hero, stateMachine, data, animBoolName)
        {
            _amountOfJumpsLeft = data.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();
        
            hero.inputHandler.UseJumpInput();
            Movement?.SetVelocityY(data.jumpVelocity);
            IsAbilityDone = true;
            _amountOfJumpsLeft--;
            hero.airState.SetIsJumping();
        }

        public bool CanJump()
        {
            return _amountOfJumpsLeft > 0;
        }

        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = data.amountOfJumps;

        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
    }
}
