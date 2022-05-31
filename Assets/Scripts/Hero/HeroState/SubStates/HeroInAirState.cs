using Core.CoreComponents;
using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.Input;
using UnityEngine;

namespace Hero.HeroState.SubStates
{
    public class HeroInAirState : HeroFiniteStateMachine.HeroState
    {
        private bool _jumpInput;
        private bool _jumpInputStop;
        private bool _grapInput;
        private int _inputX;
        private bool _dashInput;   
    
        private bool _coyotTime;
        private bool _isJumping;
        private bool _wallJumpCoyoteTime;
    
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _tempIsTouchingWall; 
        private bool _tempIsTouchingWallBack;
        private bool _isTouchingWallBack;
        private bool _isTouchingLedge;
    
        private float _startWallJumpCoyoteTime;
    
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");

        protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        public HeroInAirState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) : base(hero, stateMachine, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();

            _tempIsTouchingWall = false;
            _tempIsTouchingWallBack = false;
            _isTouchingWall = false;
            _isTouchingWallBack = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            CheckCoyoteTime();
            CheckWallJumpCoyoteTime();
        
            _inputX = hero.inputHandler.normInputX;
            _jumpInput = hero.inputHandler.jumpInput;
            _jumpInputStop = hero.inputHandler.jumpInputStop;
            _grapInput = hero.inputHandler.grabInput;
            _dashInput = hero.inputHandler.dashInput;
        
            CheckJumpMultiplier();

            if (hero.inputHandler.attackInputs[(int) CombatInputs.Primary])
            {
                stateMachine.ChangeState(hero.primarAttackState);
            }
            else if (hero.inputHandler.attackInputs[(int) CombatInputs.Secondary])
            {
                stateMachine.ChangeState(hero.secondaryAttackState);
            }
            else if (_isGrounded && Movement?.currentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(hero.landState);
            }
            else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
            {
                stateMachine.ChangeState(hero.ledgeClimbeState);
            }
            else if (_jumpInput && (_isTouchingWall ||_isTouchingWallBack || _wallJumpCoyoteTime))
            {
                StopWallJumpCoyoteTime();
                if(CollisionSenses)
                    _isTouchingWall = CollisionSenses.wallFront;
                hero.jumpWallState.DetermineWallJumpDirection(_isTouchingWall);
                stateMachine.ChangeState(hero.jumpWallState);
            }
            else if (_jumpInput && hero.jumpState.CanJump())
            {
                stateMachine.ChangeState(hero.jumpState);
            }
            else if (_isTouchingWall && _grapInput && _isTouchingLedge)
            {
                stateMachine.ChangeState(hero.wallGrabState);
            }
            else if (_isTouchingWall && _inputX == Movement?.facingDirection&& Movement?.currentVelocity.y<=0)
            {
                stateMachine.ChangeState(hero.wallSlideState);
            }
            else if (_dashInput && hero.dashState.CheckIfCanDash())
            {
                stateMachine.ChangeState(hero.dashState);
            }
            else
            {
                Movement?.CheckIfShouldFlip(_inputX);
                Movement?.SetVelocityX(data.movementVelocity * _inputX);

                if (Movement == null) return;
                hero.anim.SetFloat(YVelocity, Movement.currentVelocity.y);
                hero.anim.SetFloat(XVelocity, Mathf.Abs(Movement.currentVelocity.x));
            }
        }

        private void CheckJumpMultiplier()
        {
            if (!_isJumping) return;
            if (_jumpInputStop)
            {
                Movement?.SetVelocityY(Movement.currentVelocity.y * data.variableJumpHeightMultiplier);
                _isJumping = false;
            }
            else if(Movement?.currentVelocity.y<=0f)
            {
                _isJumping = false;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private void CheckCoyoteTime()
        {
            if (_coyotTime && Time.time > StartTime + data.coyoteTime)
            {
                _coyotTime = false;
                hero.jumpState.DecreaseAmountOfJumpsLeft();
            }
        }

        public void StartCoyoteTime() => _coyotTime = true;

        public override void DoChecks()
        {
            base.DoChecks();

            _tempIsTouchingWall = _isTouchingWall;
            _tempIsTouchingWallBack = _isTouchingWallBack;
            
            if (!CollisionSenses) return;
            _isGrounded = CollisionSenses.ground;
            _isTouchingWall = CollisionSenses.wallFront;
            _isTouchingWallBack = CollisionSenses.wallBack;
            _isTouchingLedge = CollisionSenses.ledgeHorizontal;

            if (_isTouchingWall && !_isTouchingLedge)
            {
                hero.ledgeClimbeState.SetDetectedPosition(hero.transform.position);
            }

            if (!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBack &&
                (_tempIsTouchingWall || _tempIsTouchingWallBack))
            {
                StartWallJumpCoyoteTime();
            }
        }

        private void CheckWallJumpCoyoteTime()
        {
            if (_wallJumpCoyoteTime && Time.time > _startWallJumpCoyoteTime + data.coyoteTime)
                _wallJumpCoyoteTime = false;
        }

        public void StartWallJumpCoyoteTime()
        {
            _wallJumpCoyoteTime = true;
            _startWallJumpCoyoteTime = Time.time;
        }

        public void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;
        public void SetIsJumping() => _isJumping = true;
    }
}
