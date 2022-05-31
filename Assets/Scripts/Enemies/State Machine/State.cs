using UnityEngine;

namespace Enemies.State_Machine
{
    public class State
    {
        protected FinitStateMachine stateMachine;
        protected Entity entity;
        protected Core.Core core;

        public float startTime { get; protected set; }

        protected string animBoolName;

        public State(Entity entity,FinitStateMachine stateMachine,string animBoolName)
        {
            this.entity = entity;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
            core = entity.core;
        }

        public virtual void Enter()
        {
            startTime = Time.time;
            entity.anim.SetBool(animBoolName,true);
            DoChecks();
        }

        public virtual void Exit()
        {
            entity.anim.SetBool(animBoolName,false);
        }

        public virtual void LogicUpdate()
        { }

        public virtual void PhysicsUpdate()
        {
           DoChecks();
        }

        protected virtual void DoChecks()
        { }
    }
}
