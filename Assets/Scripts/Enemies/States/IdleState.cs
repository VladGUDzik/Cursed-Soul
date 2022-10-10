using Core.CoreComponents;
using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class IdleState : State
    {
        private Movement movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses collisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected D_IdleState stateData;
        protected bool FlipAfterIdle;
        protected bool IsIdleTimeOver;
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

            if (FlipAfterIdle)
            {
                movement?.Flip();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        
            movement?.SetVelocityX(0f);
        
            if (Time.time >= startTime + IdleTime)
            {
                IsIdleTimeOver = true;
            }
        }

        public override void Enter()
        {
            base.Enter();
        
            movement?.SetVelocityX(0f);
            IsIdleTimeOver = false;
            SetRandomIdleTime();
        }

        public void SetFlipAfterIdle(bool flip)
        {
            FlipAfterIdle = flip;
        }

        private void SetRandomIdleTime()
        {
            IdleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
        }
    }
}
