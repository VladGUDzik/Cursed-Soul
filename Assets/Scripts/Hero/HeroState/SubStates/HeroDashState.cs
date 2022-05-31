using Hero.Data;
using Hero.HeroFiniteStateMachine;
using Hero.HeroState.SuperStates;
using UnityEngine;

namespace Hero.HeroState.SubStates
{
    public class HeroDashState : HeroAbiilityState
    {
        public bool canDash { get; private set; }
        private bool _isHolding;
        private bool _dashInputStop;

        private float _lastDashTime;

        private Vector2 _dashDirection;
        private Vector2 _dashDirectionInput;

        public HeroDashState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data,
            string animBoolName) : base(hero, stateMachine, data, animBoolName)
        {
        }

        public bool CheckIfCanDash()
        {
            return canDash && Time.time >= _lastDashTime + data.dashCoolDown;
        }

        public void ResetCanDash() => canDash = true;

        public override void Enter()
        {
            base.Enter();

            canDash = false;
            hero.inputHandler.UseDashInput();

            _isHolding = true;
            _dashDirection = Vector2.right * Movement.facingDirection;

            Time.timeScale = data.holdTimeScale;
            StartTime = Time.unscaledTime;
        
            hero.dashDirectionIndicator.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();

            if (Movement?.currentVelocity.y>0)
            {
                Movement?.SetVelocityY(Movement.currentVelocity.y * data.dashEndYMultiplier);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            hero.anim.SetFloat("yVelocity",Movement.currentVelocity.y);//
            hero.anim.SetFloat("xVelocity",Mathf.Abs(Movement.currentVelocity.x));//
            
            if (_isHolding)
            {
                _dashDirectionInput = hero.inputHandler.dashDirectionInput;
                _dashInputStop = hero.inputHandler.dashInputStop;

                if (_dashDirectionInput != Vector2.zero)
                {
                    _dashDirection = _dashDirectionInput;
                    _dashDirection.Normalize();
                }
                
                var angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
                hero.dashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if (_dashInputStop || Time.unscaledTime >= StartTime + data.maxHoldTime)
                {
                    _isHolding = false;
                    Time.timeScale = 1f;
                    StartTime = Time.time;
                    Movement?.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                    hero.rb.drag = data.drag;
                    Movement?.SetVelocity(data.dashVelocity, _dashDirection);
                    hero.dashDirectionIndicator.gameObject.SetActive(false);
                }
            }
            else
            {
                Movement?.SetVelocity(data.dashVelocity, _dashDirection);

                if (Time.time >= StartTime + data.dashTime)
                {
                    hero.rb.drag = 0f;
                    IsAbilityDone = true;
                    _lastDashTime = Time.time;
                }
            }
        }
    }
}