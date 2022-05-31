using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;
using UnityEngine;

namespace Hero.HeroState.SubStates
{
    public class HeroJumpWallState : HeroAbiilityState
    {
        private int _wallJumpDirection;
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");

        public HeroJumpWallState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) : base(hero, stateMachine, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            hero.inputHandler.UseJumpInput();
            hero.jumpState.ResetAmountOfJumpsLeft();
            Movement?.SetVelocity(data.wallJumpVelocity,data.wallJumpAngle,_wallJumpDirection);
            Movement?.CheckIfShouldFlip(_wallJumpDirection);
            hero.jumpState.DecreaseAmountOfJumpsLeft();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            hero.anim.SetFloat(YVelocity,Movement.currentVelocity.y);
            hero.anim.SetFloat(XVelocity,Mathf.Abs(Movement.currentVelocity.x));

            if (Time.time >=StartTime + data.wallJumpTime)
            {
                IsAbilityDone = true;
            }
        }

        public void DetermineWallJumpDirection(bool isTouchingWall)
        {
            if (isTouchingWall)
                _wallJumpDirection = -Movement.facingDirection;
            else
                _wallJumpDirection = Movement.facingDirection;
        }
    }
}
