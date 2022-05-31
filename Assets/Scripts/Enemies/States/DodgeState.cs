using Core.CoreComponents;
using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class DodgeState : State
    {
        private Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected D_DodgeState stateData;

        protected bool performCloseRangeAction;
        protected bool isHeroInMaxAgroRange;
        protected bool isGrounded;
        protected bool isDodgeOver;
    
        public DodgeState(Entity entity, FinitStateMachine stateMachine, string animBoolName,D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            isDodgeOver = false;
        
            Movement?.SetVelocity(stateData.dodgeSpeed,stateData.dodgeAngle,-Movement.facingDirection);////
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= startTime + stateData.dodgeTime &&isGrounded)
            {
                isDodgeOver = true; 
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            performCloseRangeAction = entity.CheckHeroInCloseRangeAction();
            isHeroInMaxAgroRange = entity.CheckHeroInMaxAgroRange();
            if(CollisionSenses)
                isGrounded = CollisionSenses.ground;
        }

   
    }
}
