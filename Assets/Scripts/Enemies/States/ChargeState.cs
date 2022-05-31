using Core.CoreComponents;
using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class ChargeState : State
    {
        private Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected D_ChargeState StateData;

        protected bool isHeroInMinAgroRande;
        protected bool isDetectindLedge;
        protected bool isDetectingWall;
        protected bool isChargeTimeOver;
        protected bool performCloseRangeAction;
    
        public ChargeState(Entity entity, FinitStateMachine stateMachine, string animBoolName,D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.StateData = stateData;
        }

        protected override void DoChecks()
        {
            base.DoChecks();
        
            isHeroInMinAgroRande = entity.CheckHeroInMinAgroRange();
            if (!CollisionSenses) return;
            isDetectindLedge = CollisionSenses.ledgeVertical;
            isDetectingWall = CollisionSenses.wallFront;

            performCloseRangeAction = entity.CheckHeroInCloseRangeAction();
        }

        public override void Enter()
        {
            base.Enter();
        
            isChargeTimeOver = false;
            Movement?.SetVelocityX(StateData.chargeSpeed * Movement.facingDirection);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        
            Movement?.SetVelocityX(StateData.chargeSpeed * Movement.facingDirection);

            if (Time.time >= startTime + StateData.chargeTime)
            {
                isChargeTimeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}

