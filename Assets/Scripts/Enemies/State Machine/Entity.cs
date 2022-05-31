using Core.CoreComponents;
using Enemies.DeathEnemies;
using Enemies.States.Data;
using Intermedia;
using Structs;
using UnityEngine;

namespace Enemies.State_Machine
{
    public class Entity : MonoBehaviour
    {
        protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;

        public FinitStateMachine StateMachine;
        public D_Entity entityData;
        public Animator anim { get; private set; }
        public int lastDamageDirection { get; private set; }
        public AnimationToStateMachine atsm { get; private set; }
        public Core.Core core { get; private set; }
        public Death death { get; private set; }

        [SerializeField] 
        private Transform wallCheck;
        [SerializeField] 
        private Transform ledgeCheck;
        [SerializeField] 
        private Transform heroCheck;
        [SerializeField] 
        private Transform groundCheck;
        
        private float _currentStunResistance;
        private float _currentHealth;//

        [SerializeField] private float stunDamageAmount;
     
        private float _lastDamageTime;
        
        private Vector2 _velocityWorkspace;

        private WeaponAttackDetails _attack;

        protected bool IsStunned;

        public virtual void Awake()
        {
            core = GetComponentInChildren<Core.Core>();
            
            _currentStunResistance = entityData.stunResistance;

            death = GetComponent<Death>();
            
            anim =GetComponent<Animator>();
            atsm = GetComponent<AnimationToStateMachine>();

            StateMachine = new FinitStateMachine();
        }

        public virtual void Update()
        {
            core.LogicUpdate();
            StateMachine.currentState.LogicUpdate();
            
            anim.SetFloat("yVelocity", Movement.rb.velocity.y);
            
            if (Time.time >= _lastDamageTime + entityData.stunRecoveryTime)
            {
                ResetStunResistance();
            }

            if (stunDamageAmount >= _currentHealth - _attack.damageAmount)
            {
                ApplyStun();
                stunDamageAmount = 0f;
            }
        }

        public virtual void FixedUpdate()
        {
            StateMachine.currentState.PhysicsUpdate();
            _currentHealth  = death.currentHealth;
        }

        public virtual bool CheckHeroInMinAgroRange()
        {
            return Physics2D.Raycast(heroCheck.position, transform.right, entityData.minAgroDistance,
              entityData.whatIsHero);
        }
        public virtual bool CheckHeroInMaxAgroRange()
        {
            return Physics2D.Raycast(heroCheck.position, transform.right, entityData.maxAgroDistance,
                entityData.whatIsHero);
        }
        public virtual bool CheckHeroInCloseRangeAction()
        {
            return Physics2D.Raycast(heroCheck.position,transform.right,entityData.closeRangeActionDistance,
                entityData.whatIsHero);
        }
        // protected virtual void DamageHop(float velocity)
        // {
        //     _velocityWorkspace.Set(core.Movement.rb.velocity.x,velocity);
        //     core.Movement.rb.velocity = _velocityWorkspace;
        // }

        public virtual void ResetStunResistance()
        {
            IsStunned = false;
            _currentStunResistance = entityData.stunResistance;
        }

        protected virtual void ApplyStun()
        {
            _lastDamageTime = Time.time;
        
            _currentStunResistance -= stunDamageAmount;
        
           //DamageHop(entityData.damageHopSpeed);
        
            if (_currentStunResistance <= 0)
                IsStunned = true;
        }


        public virtual void OnDrawGizmos()
        {
            if (core == null) return;
            Gizmos.color = Color.red;
            var position = wallCheck.position;
            Gizmos.DrawLine(position,
                position + (Vector3) (Vector2.right * Movement.facingDirection *
                                      entityData.wallCheckDistance));
            var position1 = ledgeCheck.position;
            Gizmos.DrawLine(position1,
                position1 + (Vector3) (Vector2.down * Movement.facingDirection *
                                       entityData.ledgeCheckDistance));

            var position2 = heroCheck.position;
            var right = Vector2.right;
            Gizmos.DrawWireSphere(position2 + (Vector3) (right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(position2 + (Vector3) (right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(position2 + (Vector3) (right * entityData.maxAgroDistance), 0.2f);
        }
    }
}
