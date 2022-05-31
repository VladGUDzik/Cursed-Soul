using Core.CoreComponents;
using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class IdleState : State
    {
        private Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected D_IdleState stateData;
        protected bool flipAfterIdle;
        protected bool isIdleTimeOver;
        protected bool IsHeroInMinAgroRange;
    
        protected float IdleTime;
    
        public IdleState(Entity entity, FinitStateMachine stateMachine, string animBoolName,D_IdleState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        protected override void DoChecks()
        {
            base.DoChecks();
            IsHeroInMinAgroRange = entity.CheckHeroInMinAgroRange();
        }

        public override void Exit()
        {
            base.Exit();

            if (flipAfterIdle)
            {
                Movement?.Flip();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        
            Movement?.SetVelocityX(0f);
        
            if (Time.time >= startTime + IdleTime)
            {
                isIdleTimeOver = true;
            }
        }

        public override void Enter()
        {
            base.Enter();
        
            Movement?.SetVelocityX(0f);
            isIdleTimeOver = false;
            SetRandomIdleTime();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public void SetFlipAfterIdle(bool flip)
        {
            flipAfterIdle = flip;
        }

        private void SetRandomIdleTime()
        {
            IdleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
        }
    }
}
