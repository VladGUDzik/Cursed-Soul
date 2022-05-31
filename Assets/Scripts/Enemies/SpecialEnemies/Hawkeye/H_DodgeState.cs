using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.SpecialEnemies.Hawkeye
{
    public class H_DodgeState : DodgeState
    {
        private Hawkeye _hawkeye;
        public H_DodgeState(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_DodgeState stateData,Hawkeye _hawkeye) : base(entity, stateMachine, animBoolName, stateData)
        {
            this._hawkeye = _hawkeye;
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

            if (!isDodgeOver) return;
            if (isHeroInMaxAgroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(_hawkeye.meleeAttackState);
            }
            else if (isHeroInMaxAgroRange &&!performCloseRangeAction)
            {
                stateMachine.ChangeState(_hawkeye.rangeAttackState);
            }
            else if (!isHeroInMaxAgroRange)
            {
                stateMachine.ChangeState(_hawkeye.lookForHeroState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        protected override void DoChecks()
        {
            base.DoChecks();
        }
    }
}
