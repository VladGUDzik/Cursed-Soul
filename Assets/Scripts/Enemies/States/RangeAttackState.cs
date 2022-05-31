using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class RangeAttackState : AttackState
    {
        protected D_RangeAttackState rangeAttackState;

        protected GameObject Projectile;
        protected Projectile.Projectile ProjectileScript;
    
        public RangeAttackState(Entity entity, FinitStateMachine stateMachine, string animBoolName, Transform attackPosition,
            D_RangeAttackState rangeAttackState) : base(entity, stateMachine, animBoolName, attackPosition)
        {
            this.rangeAttackState = rangeAttackState;
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

            Projectile= Object.Instantiate(rangeAttackState.projectile, attackPosition.position,
                attackPosition.rotation);
            ProjectileScript = Projectile.GetComponent<Projectile.Projectile>();
            ProjectileScript.FireProjectile(rangeAttackState.projectileSpeed,rangeAttackState.projectileTravelDistance,
                rangeAttackState.projectileDamage);
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}
