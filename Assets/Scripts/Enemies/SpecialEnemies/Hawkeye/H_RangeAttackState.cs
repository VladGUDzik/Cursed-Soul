using Audio;
using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.SpecialEnemies.Hawkeye
{
    public class H_RangeAttackState : RangeAttackState
    {
        private Hawkeye _hawkeye;
    
        public H_RangeAttackState(Entity entity, FinitStateMachine stateMachine, string animBoolName, Transform attackPosition,
            D_RangeAttackState rangeAttackState,Hawkeye _hawkeye) : base(entity, stateMachine, animBoolName, attackPosition, rangeAttackState)
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
           
            if (!isAnimationFinished) return;
           
            if (isHeroInMinAgroRange)
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

        public override void TriggerAttack()
        {
            base.TriggerAttack();
            SoundManager.instance.PlaySound(_hawkeye.arrowFireSound);
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}
