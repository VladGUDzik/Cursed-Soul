using Core.CoreComponents;
using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.Input;

namespace Hero.HeroState.SuperStates
{
    public class HeroGroundedState : HeroFiniteStateMachine.HeroState
    {
        protected int InputX;
    
        private bool _grapInput;
        private bool _jumpInput;
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _isTouchingLedge;
        private bool _dashInput;

        protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        public HeroGroundedState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data,
            string animBoolName) : base(hero, stateMachine, data, animBoolName)
        { }

        public override void Enter()
        {
            base.Enter();
        
            hero.jumpState.ResetAmountOfJumpsLeft();
            hero.dashState.ResetCanDash();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            InputX = hero.inputHandler.normInputX;
            _jumpInput = hero.inputHandler.jumpInput;
            _grapInput = hero.inputHandler.grabInput;
            _dashInput = hero.inputHandler.dashInput;

            if (hero.inputHandler.attackInputs[(int) CombatInputs.Primary])
            {
                stateMachine.ChangeState(hero.primarAttackState);
            }
            else if (hero.inputHandler.attackInputs[(int) CombatInputs.Secondary])
            {
                stateMachine.ChangeState(hero.secondaryAttackState);
            }
            else if (_jumpInput && hero.jumpState.CanJump())
            {
                stateMachine.ChangeState(hero.jumpState);
            }
            else if (!_isGrounded)
            {
                hero.airState.StartCoyoteTime();
                stateMachine.ChangeState(hero.airState);
            }
            else if (_isTouchingWall && _grapInput && _isTouchingLedge)
            {
                stateMachine.ChangeState(hero.wallGrabState);
            }
            else if (_dashInput && hero.dashState.CheckIfCanDash())
            {
                stateMachine.ChangeState(hero.dashState);
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
            _isTouchingWall = CollisionSenses.wallFront;
            _isGrounded = CollisionSenses.ground;
            _isTouchingLedge = CollisionSenses.ledgeHorizontal;
        }
    }
}
