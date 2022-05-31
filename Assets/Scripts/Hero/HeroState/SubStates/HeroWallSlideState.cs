using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;

namespace Hero.HeroState.SubStates
{
    public class HeroWallSlideState : HeroWallTouchingState
    {
        public HeroWallSlideState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) : base(hero, stateMachine, data, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Movement?.SetVelocityY(-data.wallSlideVelocity);
            if (GrabInput && yInput == 0)
            {
                stateMachine.ChangeState(hero.wallGrabState);
            }
        }
       
    }
}

