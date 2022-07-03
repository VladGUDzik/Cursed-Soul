using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.SpecialEnemies.Hawkeye
{
   public class Hawkeye : Entity
   {
      public H_MoveState moveState { get; private set; }
      public H_IdleState idleState { get; private set; }
      public H_HeroDetectedState heroDetectedState { get; private set; }
      public H_MeleeAttackState meleeAttackState { get; private set; }
      public H_LookForHeroState lookForHeroState { get; private set; }
      public H_StunState stunState { get; private set; }
      public H_DodgeState dodgeState { get; private set; }
      public H_RangeAttackState rangeAttackState { get; private set; }

      [SerializeField]
      private D_MoveState moveStateData;
      [SerializeField]
      private D_IdleState idleStateData;
      [SerializeField] 
      private D_HeroDetected heroDetectedStateData;
      [SerializeField] 
      private D_MeleeAttack meleeAttackStateData; 
      [SerializeField] 
      private D_LookForHero lookForHeroStateData;
      [SerializeField] 
      private D_StunState stunStateData;
      [SerializeField] 
      public D_DodgeState dodgeStateData;
      [SerializeField] 
      public D_RangeAttackState rangeAttackStateData;

      [SerializeField] 
      private Transform meleeAttackPosition;
      [SerializeField] 
      private Transform rangeAttackPosition;
      
      [SerializeField] protected internal AudioClip meleeAttackSound,arrowFireSound;
      public override void Awake()
      {
         base.Awake();
      
         moveState = new H_MoveState(this,StateMachine,"move",moveStateData,this);
         idleState = new H_IdleState(this, StateMachine, "idle", idleStateData, this);
         heroDetectedState = new H_HeroDetectedState(this, StateMachine, "heroDetected", heroDetectedStateData, this);
         meleeAttackState = new H_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackPosition,meleeAttackStateData, this);
         lookForHeroState = new H_LookForHeroState(this, StateMachine, "lookForHero", lookForHeroStateData, this);
         stunState = new H_StunState(this, StateMachine, "stun", stunStateData, this);
         dodgeState = new H_DodgeState(this, StateMachine, "dodge", dodgeStateData, this);
         rangeAttackState = new H_RangeAttackState(this, StateMachine, "rangeAttack", rangeAttackPosition,rangeAttackStateData, this);
      
      }

      private void Start()
      {
         StateMachine.Initialize(moveState);
      }

      // ReSharper disable Unity.PerformanceAnalysis
      protected override void ApplyStun()
      {
         base.ApplyStun();
         
          if (IsStunned && StateMachine.currentState != stunState)
         {
            StateMachine.ChangeState(stunState);
         }
         else if (CheckHeroInMinAgroRange())
         {
            StateMachine.ChangeState(rangeAttackState);
         }
         else if (!CheckHeroInMinAgroRange())
         {
            lookForHeroState.SetTurnImmediatly(true);
            StateMachine.ChangeState(lookForHeroState);
         }
      }

      public override void OnDrawGizmos()
      {
         base.OnDrawGizmos();
      
         Gizmos.color=Color.red;
         Gizmos.DrawWireSphere(meleeAttackPosition.position,meleeAttackStateData.attackRadius);
      }
   }
}