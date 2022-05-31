using Core.CoreComponents;
using Enemies.State_Machine;
using Enemies.States.Data;

namespace Enemies.States
{
    public class MoveState : State
    {
        protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        protected D_MoveState StateData;

        protected bool IsDetectingWall;
        protected bool IsDetectingLedge;
        protected bool IsHeroInMinAgroRange;
        protected bool IsOtherEnemy;//
    
        public MoveState(Entity entity, FinitStateMachine stateMachine, string animBoolName,D_MoveState StateData) :
            base(entity, stateMachine, animBoolName)
        {
            this.StateData = StateData;
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            if (!CollisionSenses) return;
            IsDetectingLedge = CollisionSenses.ledgeVertical;
            IsDetectingWall = CollisionSenses.wallFront;
            IsOtherEnemy = CollisionSenses.otherEnemyOnTheWay;//
            
            IsHeroInMinAgroRange = entity.CheckHeroInMinAgroRange();
        }

        public override void Enter()
        {
            base.Enter();
            Movement?.SetVelocityX(StateData.movementSpeed * Movement.facingDirection);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Movement?.SetVelocityX(StateData.movementSpeed * Movement.facingDirection);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    
    }
}
