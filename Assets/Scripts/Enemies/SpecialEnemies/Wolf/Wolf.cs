using Enemies.State_Machine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.SpecialEnemies.Wolf
{
   public class Wolf : Entity
   {
      public Wolf_IdleState idleState { get; private set; }
      public Wolf_MoveState moveState { get; private set; }
      public Wolf_HeroDetectedState heroDetectedState { get; private set; }
      public Wolf_ChargeState chargeState { get; private set; }
      public Wolf_LookForHero lookForHeroState { get; private set; }
      public Wolf_MeleeAttackState meleeAttackState { get; private set; }
      public Wolf_StunState stunState { get; private set; }
      

      [SerializeField] private D_IdleState idleStateData;
      [SerializeField] private D_MoveState moveStateData;
      [SerializeField] private D_HeroDetected heroDetectedData;
      [SerializeField] private D_ChargeState chargeStateData;
      [SerializeField] private D_LookForHero lookForHeroData;
      [SerializeField] private D_MeleeAttack meleeAttackStateData;
      [SerializeField] private D_StunState stunStateData;

      [SerializeField] private Transform meleeAttackPosition;

      [SerializeField] protected internal AudioClip meleeAttackSound;
      public override void Awake()
      {
         base.Awake();
         moveState = new Wolf_MoveState(this, StateMachine, "move", moveStateData, this);
         idleState = new Wolf_IdleState(this, StateMachine, "idle", idleStateData, this);
         heroDetectedState = new Wolf_HeroDetectedState(this, StateMachine, "heroDetected",
            heroDetectedData, this);
         chargeState = new Wolf_ChargeState(this, StateMachine, "charge", chargeStateData, this);
         lookForHeroState = new Wolf_LookForHero(this, StateMachine, "lookForHero", lookForHeroData, this);
         meleeAttackState = new Wolf_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackPosition,
            meleeAttackStateData, this);
         stunState = new Wolf_StunState(this, StateMachine, "stun", stunStateData, this);
      }

      private void Start()
      {
         StateMachine.Initialize(moveState);
      }

      protected override void ApplyStun()
      {
         base.ApplyStun();
         if (IsStunned && StateMachine.currentState != stunState)
         {
            StateMachine.ChangeState(stunState);
         }
      }

      public override void OnDrawGizmos()
      {
         Gizmos.color=Color.red;
         Gizmos.DrawWireSphere(meleeAttackPosition.position,meleeAttackStateData.attackRadius);
      }
 
   }
}
