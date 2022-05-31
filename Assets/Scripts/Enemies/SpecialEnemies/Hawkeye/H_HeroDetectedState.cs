using Audio;
using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.SpecialEnemies.Hawkeye
{
    public class H_HeroDetectedState : HeroDetectedState
    {
        private Hawkeye _hawkeye;
    
        public H_HeroDetectedState(Entity entity, FinitStateMachine stateMachine, string animBoolName, D_HeroDetected stateData,Hawkeye _hawkeye) : base(entity, stateMachine, animBoolName, stateData)
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

            if (PerformCloseRangeAction)
            {
                if (Time.time >= _hawkeye.dodgeState.startTime + _hawkeye.dodgeStateData.dodgeCooldown)
                {
                    stateMachine.ChangeState(_hawkeye.dodgeState);
                }
                else
                {
                    stateMachine.ChangeState(_hawkeye.meleeAttackState);
                }
            }
            else if (PerformLongRangeAction)
            {
                stateMachine.ChangeState(_hawkeye.rangeAttackState);
            }
            else if (!IsheroInMaxAgroRange)
            {
                stateMachine.ChangeState(_hawkeye.lookForHeroState);
            }
        }
    }
}
