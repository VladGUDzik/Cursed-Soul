using Hero.Data;
using UnityEngine;

namespace Hero.HeroFiniteStateMachine
{
    public class HeroState
    {
        protected Core.Core core;
    
        protected Hero hero;
        protected HeroStateMachine stateMachine;
        protected HeroData data;

        protected bool IsAnimationFinished;
        protected bool IsExitingState;
    
        protected float StartTime;
    
        private string _animBoolName;

        public HeroState(Hero hero, HeroStateMachine stateMachine, HeroData data, string animBoolName)
        {
            this.hero = hero;
            this.stateMachine = stateMachine;
            this.data = data;
            _animBoolName = animBoolName;
            core = hero.core;
        }

        public virtual void Enter()
        {
            DoChecks();
            hero.anim.SetBool(_animBoolName,true);
            StartTime = Time.time;
            IsAnimationFinished = false;
            IsExitingState = false;
        }
    
        public virtual void Exit()
        {
            hero.anim.SetBool(_animBoolName,false);
            IsExitingState = true;
        }
    
        public virtual void LogicUpdate()
        { } 
    
        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }

        public virtual void DoChecks()
        { }
        public virtual void AnimationTrigger()
        { }
        public virtual void AnimationFinishTrigger() 
            => IsAnimationFinished = true;


    }
}
