using Audio;
using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.SpecialEnemies.Hawkeye
{
    public class H_MeleeAttackState : MeleeAtackState
    {
        private Hawkeye _hawkeye;
    
        public H_MeleeAttackState(Entity entity, FinitStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack StateData,Hawkeye _hawkeye) : base(entity, stateMachine, animBoolName, attackPosition, StateData)
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
                stateMachine.ChangeState(_hawkeye.heroDetectedState);
            else if (!isHeroInMinAgroRange)
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
            SoundManager.instance.PlaySound(_hawkeye.meleeAttackSound);
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}
