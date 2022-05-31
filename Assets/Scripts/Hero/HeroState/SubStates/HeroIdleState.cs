using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;

namespace Hero.HeroState.SubStates
{
    public class HeroIdleState : HeroGroundedState
    {
        public HeroIdleState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) 
            : base(hero, stateMachine, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        
            Movement?.SetVelocityX(0f);
        }
    
        public override void Exit()
        {
            base.Exit();
        }
    
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (InputX != 0 && !IsExitingState )
            {
                stateMachine.ChangeState(hero.moveState);
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
