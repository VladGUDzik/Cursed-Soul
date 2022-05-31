using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;

namespace Hero.HeroState.SubStates
{
    public class HeroLandState : HeroGroundedState
    {
        public HeroLandState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) :
            base(hero, stateMachine, data, animBoolName)
        { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            if (InputX != 0)
            {
                stateMachine.ChangeState(hero.moveState);
            }
            else if (IsAnimationFinished)
            {
                stateMachine.ChangeState(hero.idleState);
            }
        }
    }
}
