using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.SpecialEnemies.Hawkeye
{
    public class H_IdleState : IdleState
    {
        private Hawkeye _hawkeye;
    
        public H_IdleState(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_IdleState stateData,Hawkeye _hawkeye) : base(entity, stateMachine, animBoolName, stateData)
        {
            this._hawkeye = _hawkeye;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
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

            if (IsHeroInMinAgroRange)
                stateMachine.ChangeState(_hawkeye.heroDetectedState);
            else if (isIdleTimeOver)
                stateMachine.ChangeState(_hawkeye.moveState);
        }
    }
}
