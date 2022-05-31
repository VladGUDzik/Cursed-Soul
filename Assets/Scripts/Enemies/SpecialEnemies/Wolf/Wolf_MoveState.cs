using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.SpecialEnemies.Wolf
{
    public class Wolf_MoveState : MoveState
    {
        private Wolf _wolf;
        public Wolf_MoveState(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_MoveState StateData,Wolf _wolf) : base(entity, stateMachine, animBoolName, StateData)
        {
            this._wolf = _wolf;
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
                stateMachine.ChangeState(_wolf.heroDetectedState);
            }
            else if (IsDetectingWall || !IsDetectingLedge || IsOtherEnemy)//
            {
                _wolf.idleState.SetFlipAfterIdle(true);
                stateMachine.ChangeState(_wolf.idleState);
            }
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
