using Core.CoreComponents;
using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class LookForHeroState : State
    {
        private Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        
        protected D_LookForHero stateData;

        protected bool turnImmediately;
        protected bool isHeroInMinAgroRange;
        protected bool isAllTurnsDone;
        protected bool isAllTurnsTimeDone;

        protected float lastTurnTime;

        protected int amountOfTurnsDone;
    
        public LookForHeroState(Entity entity, FinitStateMachine stateMachine, string animBoolName,D_LookForHero stateData)
            : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            isHeroInMinAgroRange = entity.CheckHeroInMinAgroRange();
        }

        public override void Enter()
        {
            base.Enter();

            isAllTurnsDone = false;
            isAllTurnsTimeDone = false;

            lastTurnTime = startTime;
            amountOfTurnsDone = 0;
        
            Movement?.SetVelocityX(0f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Movement?.SetVelocityX(0f);
            if (turnImmediately)
            {
                Movement?.Flip();
                lastTurnTime=Time.time;
                amountOfTurnsDone++;
                turnImmediately = false;
            }
            else if(Time.time>=lastTurnTime+stateData.timeBetweenTurns&& !isAllTurnsDone)
            {
                Movement?.Flip();
                lastTurnTime = Time.time;
                amountOfTurnsDone++;
            }

            if (amountOfTurnsDone > stateData.amountOfTurns)
            {
                isAllTurnsDone = true;
            }

            if (Time.time >= lastTurnTime + stateData.timeBetweenTurns&&isAllTurnsDone)
            {
                isAllTurnsTimeDone = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public void SetTurnImmediatly(bool flip)
        {
            turnImmediately = flip;
        }
    }
}
