using Core.CoreComponents;
using Hero.Data;
using Hero.HeroFiniteStateMachine;
using UnityEngine;

namespace Hero.HeroState.SubStates
{
    public class HeroLedgeClimbeState : HeroFiniteStateMachine.HeroState
    {
        private Vector2 _detectedPos;
        private Vector2 _cornerPos;
        private Vector2 _startPos;
        private Vector2 _stopPos;
        private Vector2 _workSpace;

        private bool _isHanging;
        private bool _isClimbing;
        private bool _jumpInput;

        private int _xInput;
        private int _yInput;
        private static readonly int ClimbLedge = Animator.StringToHash("climbLedge");

        protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        public HeroLedgeClimbeState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) : base(
            hero, stateMachine, data, animBoolName)
        {
        }

        public void SetDetectedPosition(Vector2 pos) => _detectedPos = pos;

        public override void Enter()
        {
            base.Enter();

            Movement.SetVelocityZero();
            var position = hero.transform.position;
            position = _detectedPos;
            _cornerPos = DetermineCornePosition();

            if (Movement != null)
            {
                _startPos.Set(_cornerPos.x - (Movement.facingDirection * data.startOffset.x),
                    _cornerPos.y - data.startOffset.y);

                _stopPos.Set(_cornerPos.x + (Movement.facingDirection * data.startOffset.x),
                    _cornerPos.y + data.stopOffset.y);
            }

            position = _startPos;
            hero.transform.position = position;
        }

        public override void Exit()
        {
            base.Exit();

            _isHanging = false;
            if (_isClimbing)
            {
                hero.transform.position = _stopPos;
                _isClimbing = false;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsAnimationFinished)
            {
                stateMachine.ChangeState(hero.idleState);
            }
            else
            {
                _xInput = hero.inputHandler.normInputX;
                _yInput = hero.inputHandler.normInputY;
                _jumpInput = hero.inputHandler.jumpInput;

                Movement?.SetVelocityZero();
                hero.transform.position = _startPos;

                if (_xInput == Movement?.facingDirection && _isHanging && !_isClimbing)
                {
                    _isClimbing = true;
                    hero.anim.SetBool(ClimbLedge, true);
                }
                else if (_yInput == -1 && _isHanging && !_isClimbing)
                {
                    stateMachine.ChangeState(hero.airState);
                }
                else if (_jumpInput && !_isClimbing)
                {
                    hero.jumpWallState.DetermineWallJumpDirection(true);
                    stateMachine.ChangeState(hero.jumpWallState);
                }
            }
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();

            _isHanging = true;
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        
            hero.anim.SetBool("climbLedge",false);
        }
    
        private Vector2 DetermineCornePosition()
        {
            var wallCheckPosition = CollisionSenses.WallCheck.position;
            var ledgeCheckPosition = CollisionSenses.LedgeCheckHorizontal.position;
        
            var xHit = Physics2D.Raycast(wallCheckPosition, Vector2.right * Movement.facingDirection,
                CollisionSenses.WallCheckDistance, CollisionSenses.WhatIsGround);
            var xDist = xHit.distance;
            _workSpace.Set(xDist*Movement.facingDirection,0f);
         
            var yHit = Physics2D.Raycast(ledgeCheckPosition + (Vector3) _workSpace, Vector2.down,
                ledgeCheckPosition.y - wallCheckPosition.y, CollisionSenses.WhatIsGround);
            var yDist = yHit.distance;
         
            _workSpace.Set(wallCheckPosition.x +(xDist*Movement.facingDirection),ledgeCheckPosition.y -yDist);
            return _workSpace;
        }
    }
}