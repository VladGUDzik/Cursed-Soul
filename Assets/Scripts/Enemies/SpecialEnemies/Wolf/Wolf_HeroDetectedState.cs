using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.SpecialEnemies.Wolf
{
    public class Wolf_HeroDetectedState : HeroDetectedState
    {
        private Wolf _wolf;
    
        public Wolf_HeroDetectedState(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_HeroDetected stateData,Wolf _wolf) : base(entity, stateMachine, animBoolName, stateData)
        {
            this._wolf = _wolf;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (PerformCloseRangeAction)
            {
                stateMachine.ChangeState(_wolf.meleeAttackState);
            }
            else if (PerformLongRangeAction)
            {
                stateMachine.ChangeState(_wolf.chargeState);
            }
            else if (!IsheroInMaxAgroRange)
            {
                stateMachine.ChangeState(_wolf.lookForHeroState);
            }
            else if (!isDetectingLedge)
            {
                Movement.Flip();
                stateMachine.ChangeState(_wolf.moveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
