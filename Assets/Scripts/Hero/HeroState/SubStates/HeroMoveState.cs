using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;

namespace Hero.HeroState.SubStates
{
    public class HeroMoveState : HeroGroundedState
    {
        public HeroMoveState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) : base(hero, stateMachine, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            Movement?.CheckIfShouldFlip(InputX);
        
            Movement?.SetVelocityX(data.movementVelocity * InputX);
      
            if (InputX == 0 && !IsExitingState)
            {
                stateMachine.ChangeState(hero.idleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}
