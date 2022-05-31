using Core.CoreComponents;
using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class StunState : State
    {
        private Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        protected D_StunState stateData;

        protected bool IsStunTimeOver;
        protected bool IsGrounded;
        protected bool IsMovementStopped;
        protected bool PerformCloseRangeAction;
        protected bool IsHeroInMinAgroRange;
    
        public StunState(Entity entity, FinitStateMachine stateMachine, string animBoolName,D_StunState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            IsStunTimeOver = false;
            IsMovementStopped = false;
            Movement?.SetVelocity(stateData.stunKnockbackSpeed,stateData.stunKnockbackAngle,entity.lastDamageDirection);
            
        }

        public override void Exit()
        {
            base.Exit();
            entity.ResetStunResistance();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= startTime + stateData.stunTime)
            {
                IsStunTimeOver = true;
            }

            if (IsGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !IsMovementStopped)
            {
                IsMovementStopped = true;
                Movement?.SetVelocityX(0f);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        protected override void DoChecks()
        {
            base.DoChecks();
            
            if (CollisionSenses)
                IsGrounded = CollisionSenses.ground;
            
            PerformCloseRangeAction = entity.CheckHeroInCloseRangeAction();
            IsHeroInMinAgroRange = entity.CheckHeroInMinAgroRange();
        }
    }
}
