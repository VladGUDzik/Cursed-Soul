using Audio;
using Enemies.State_Machine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.SpecialEnemies.Wolf
{
    public class Wolf_MeleeAttackState : MeleeAtackState
    {
        private Wolf _wolf;
    
        public Wolf_MeleeAttackState(Entity entity, FinitStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack StateData,Wolf _wolf) : base(entity, stateMachine, animBoolName, attackPosition, StateData)
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

            if (!isAnimationFinished) return;
            if (isHeroInMinAgroRange)
            {
                stateMachine.ChangeState(_wolf.heroDetectedState);
            }
            else
            {
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

        public override void TriggerAttack()
        {
            base.TriggerAttack();
            SoundManager.instance.PlaySound(_wolf.meleeAttackSound);
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}
