using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.SpecialEnemies.Wolf
{
    public class Wolf_LookForHero : LookForHeroState
    {
        private Wolf _wolf;
        public Wolf_LookForHero(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_LookForHero stateData,Wolf _wolf) 
            : base(entity, stateMachine, animBoolName, stateData)
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
            if (isHeroInMinAgroRange)
            {
                stateMachine.ChangeState(_wolf.heroDetectedState);
            }
            else if(isAllTurnsTimeDone)
            {
                stateMachine.ChangeState(_wolf.moveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}

