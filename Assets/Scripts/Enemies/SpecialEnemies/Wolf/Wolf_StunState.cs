using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.SpecialEnemies.Wolf
{
    public class Wolf_StunState : StunState
    {
        private Wolf _wolf;
    
        public Wolf_StunState(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_StunState stateData,Wolf _wolf) : base(entity, stateMachine, animBoolName, stateData)
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

            if (!IsStunTimeOver) return;

            if (PerformCloseRangeAction)
            {
                stateMachine.ChangeState(_wolf.meleeAttackState);
            }
            else if (IsHeroInMinAgroRange)
            {
                stateMachine.ChangeState(_wolf.chargeState);
            }
            else
            {
                _wolf.lookForHeroState.SetTurnImmediatly(true);
                stateMachine.ChangeState(_wolf.lookForHeroState);
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
