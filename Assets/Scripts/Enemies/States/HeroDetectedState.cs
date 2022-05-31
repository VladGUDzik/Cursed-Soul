using Core.CoreComponents;
using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class HeroDetectedState : State
    {
        protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected D_HeroDetected stateData;

        protected bool IsheroInMinAgroRange;
        protected bool IsheroInMaxAgroRange;
        protected bool PerformLongRangeAction;
        protected bool PerformCloseRangeAction;
        protected bool isDetectingLedge;
    
        public HeroDetectedState(Entity entity, FinitStateMachine stateMachine, string animBoolName,D_HeroDetected stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData; 
        }

        protected override void DoChecks()
        {
            base.DoChecks();
            
            IsheroInMinAgroRange = entity.CheckHeroInMinAgroRange();
            IsheroInMaxAgroRange = entity.CheckHeroInMaxAgroRange();
            if(CollisionSenses)
                isDetectingLedge = CollisionSenses.ledgeVertical;

            PerformCloseRangeAction = entity.CheckHeroInCloseRangeAction();
        }

        public override void Enter()
        {
            base.Enter();

            PerformLongRangeAction = false;
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
            
            if (Time.time >= startTime + stateData.longRangeActionTime)
            {
                PerformLongRangeAction = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
