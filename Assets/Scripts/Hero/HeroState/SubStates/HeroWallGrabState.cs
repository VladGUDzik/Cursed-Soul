using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;
using UnityEngine;

namespace Hero.HeroState.SubStates
{
    public class HeroWallGrabState : HeroWallTouchingState
    {
        private Vector2 _holdPosition;
        public HeroWallGrabState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) : base(hero, stateMachine, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _holdPosition = hero.transform.position;
        
            HoldPosition();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        
            if (IsExitingState) return;
        
            HoldPosition();
            if (yInput > 0)
            {
                stateMachine.ChangeState(hero.wallClimbState);
            }
            else if (yInput < 0 || !GrabInput)
            {
                stateMachine.ChangeState(hero.wallSlideState);
            }
        }

        private void HoldPosition()
        {
            hero.transform.position = _holdPosition;
        
            Movement?.SetVelocityX(0f);
            Movement?.SetVelocityY(0f);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }
    }
}
