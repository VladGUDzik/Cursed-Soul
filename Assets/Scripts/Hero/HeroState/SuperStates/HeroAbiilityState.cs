using Core.CoreComponents;
using Hero.Data;
using Hero.HeroFiniteStateMachine;

namespace Hero.HeroState.SuperStates
{
    public class HeroAbiilityState : HeroFiniteStateMachine.HeroState
    {
        protected bool IsAbilityDone;
        private bool _isGrounded;
        
        protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        public HeroAbiilityState(Hero.HeroFiniteStateMachine.Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName) : base(hero, stateMachine, data, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            IsAbilityDone = false;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsAbilityDone) return;
            if (_isGrounded && Movement?.currentVelocity.y < 0.01f)
            {
                stateMachine.ChangeState(hero.idleState);
            }
            else
            {
                stateMachine.ChangeState(hero.airState);
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
            _isGrounded = CollisionSenses.ground;
        }
    }
}
