using Core.CoreComponents;
using Enemies.State_Machine;
using Enemies.States.Data;
using Interfaces;
using UnityEngine;

namespace Enemies.States
{
    public class MeleeAtackState : AttackState
    {
        protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        protected D_MeleeAttack StateData;
    
        public MeleeAtackState(Entity entity, FinitStateMachine stateMachine, string animBoolName, Transform attackPosition,D_MeleeAttack StateData) : base(entity, stateMachine, animBoolName, attackPosition)
        {
            this.StateData = StateData;
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

            var detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position,StateData.attackRadius,
                StateData.whatIsHero);

            foreach (var collider in detectedObjects)
            {
                var damageable = collider.GetComponent<IDamageable>();
                damageable?.Damage(StateData.attackDamage);

                var knockbackable = collider.GetComponent<IKnockbackable>();
                knockbackable?.Knockback(StateData.knockbackAngle, StateData.knockbackStrength,
                    Movement.facingDirection);
            }
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}
