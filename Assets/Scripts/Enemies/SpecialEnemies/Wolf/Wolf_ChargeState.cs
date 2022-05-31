using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.SpecialEnemies.Wolf
{
    public class Wolf_ChargeState : ChargeState
    {
        private Wolf _wolf;
    
        public Wolf_ChargeState(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_ChargeState stateData,Wolf _wolf) : base(entity, stateMachine, animBoolName, stateData)
        {
            this._wolf = _wolf;
        }

        protected override void DoChecks()
        {
            base.DoChecks();
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

            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(_wolf.meleeAttackState);
            }
            else if (!isDetectindLedge || isDetectingWall)
            {
                stateMachine.ChangeState(_wolf.lookForHeroState);
            }
            else if (isChargeTimeOver)
            {
                if (isHeroInMinAgroRande)
                {
                    stateMachine.ChangeState(_wolf.heroDetectedState);
                }
                else
                {
                    stateMachine.ChangeState(_wolf.lookForHeroState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
