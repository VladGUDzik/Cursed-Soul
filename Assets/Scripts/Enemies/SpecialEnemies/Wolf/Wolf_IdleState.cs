using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.SpecialEnemies.Wolf
{
    public class Wolf_IdleState : IdleState
    {
        private Wolf Wolf;
        public Wolf_IdleState(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_IdleState stateData,Wolf Wolf) : base(entity, stateMachine, animBoolName, stateData)
        {
            this.Wolf = Wolf;
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
            {
                stateMachine.ChangeState(Wolf.heroDetectedState);
            }
            else if (IsIdleTimeOver)
            {
                stateMachine.ChangeState(Wolf.moveState);
            }
        }

        public override void PhysicsUpdate()
        {
        }
    }
}
