using Core.CoreComponents;
using Hero.Data;
using Hero.HeroFiniteStateMachine;

namespace Hero.HeroState.SuperStates
{
    public class HeroWallTouchingState : HeroFiniteStateMachine.HeroState
    {
        protected bool IsGrounded;
        protected bool IsTouchingWall;
        protected bool GrabInput;
        protected bool JumpInput;
        protected int xInput;
        protected int yInput;
        protected bool IsTouchingLedge;
    
        protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        public HeroWallTouchingState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) : base(hero, stateMachine, data, animBoolName)
        {
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

            xInput = hero.inputHandler.normInputX;
            yInput = hero.inputHandler.normInputY;
            GrabInput = hero.inputHandler.grabInput;
            JumpInput = hero.inputHandler.jumpInput;
        
            if (JumpInput)
            {
                hero.jumpWallState.DetermineWallJumpDirection(IsTouchingWall);
                stateMachine.ChangeState(hero.jumpWallState);
            }
            else if (IsGrounded && !GrabInput)
            {
                stateMachine.ChangeState(hero.idleState);
            }
            else if (!IsTouchingWall || (xInput != Movement?.facingDirection &&!GrabInput))
            {
                stateMachine.ChangeState(hero.airState);
            }
            else if (IsTouchingWall && !IsTouchingLedge)
            {
                stateMachine.ChangeState(hero.ledgeClimbeState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
            
            if (!CollisionSenses) return;
            IsGrounded = CollisionSenses.ground;
            IsTouchingWall = CollisionSenses.wallFront;
            IsTouchingLedge = CollisionSenses.ledgeHorizontal;

            if (IsTouchingWall && !IsTouchingLedge)
            {
                hero.ledgeClimbeState.SetDetectedPosition(hero.transform.position);
            }
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }
    }
}
