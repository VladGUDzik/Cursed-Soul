using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;

namespace Hero.HeroState.SubStates
{
    public class HeroWallClimbState : HeroWallTouchingState
    {
        public HeroWallClimbState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) : base(hero, stateMachine, data, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            Movement?.SetVelocityY(data.wallClimbVelocity);
                 
            if (yInput != 1 )
            {
                stateMachine.ChangeState(hero.wallGrabState);
            }
        }
    }
}
