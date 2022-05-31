using Core.CoreComponents;
using Enemies.State_Machine;
using UnityEngine;

namespace Enemies.States
{
    public class AttackState : State
    {
        
        private Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;

        protected Transform attackPosition;

        protected bool isAnimationFinished;
        protected bool isHeroInMinAgroRange;
    
        public AttackState(Entity entity, FinitStateMachine stateMachine, string animBoolName,Transform attackPosition) : base(entity, stateMachine, animBoolName)
        {
            this.attackPosition = attackPosition;
        }

        public override void Enter()
        {
            base.Enter();

            entity.atsm.AttackState = this;
            isAnimationFinished = false;
            Movement?.SetVelocityX(0f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Movement?.SetVelocityX(0f);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            isHeroInMinAgroRange = entity.CheckHeroInMinAgroRange();
        }

        public virtual void TriggerAttack()
        {
        } 
        public virtual void FinishAttack()
        {
            isAnimationFinished = true;
        }
    }
}
