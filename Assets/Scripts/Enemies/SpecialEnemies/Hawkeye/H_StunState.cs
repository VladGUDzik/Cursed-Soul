using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.SpecialEnemies.Hawkeye
{
    public class H_StunState : StunState
    {
        private Hawkeye _hawkeye;
    
        public H_StunState(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_StunState stateData,Hawkeye _hawkeye) : base(entity, stateMachine, animBoolName, stateData)
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

            if (!IsStunTimeOver) return;
            if (IsHeroInMinAgroRange)
            {
                stateMachine.ChangeState(_hawkeye.heroDetectedState);
            }
            else
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
