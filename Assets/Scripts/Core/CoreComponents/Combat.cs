using Health.HeroDeath;
using Interfaces;
using UnityEngine;

namespace Core.CoreComponents
{
    public class Combat : CoreComponent,IKnockbackable
    {
        protected Movement Movement => _movement ??= Core.GetCoreComponent(ref _movement);
        private Movement _movement;
        private CollisionSenses CollisionSenses => _collisionSenses ??= Core.GetCoreComponent(ref _collisionSenses);
        private CollisionSenses _collisionSenses;
        
        private bool _isKnockbackActive;
        private float _knockbackStartTime;
        
        [SerializeField]private float maxKnockbackTime = 0.2f;
        [SerializeField] private Vector2 angle;
        [SerializeField] private float touchDamageAmount,damageVelocity;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void LogicUpdate()
        {
            CheckKnockback();
        }

        public void Knockback(Vector2 angle, float strength, int direction)
        {
            Movement?.SetVelocity(strength,angle,direction);
            if (Movement != null) Movement.CanSetVelocity = false;
            _isKnockbackActive = true;
            _knockbackStartTime = Time.time;
        }
    
        private void CheckKnockback()
        {
            if (_isKnockbackActive && Movement.currentVelocity.y <= 0.01f 
                                   && (CollisionSenses.ground || Time.time >=_knockbackStartTime+maxKnockbackTime))
            {
                _isKnockbackActive = false;
                Movement.CanSetVelocity = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            other.GetComponent<DeathHero>().Damage(touchDamageAmount);
            other.GetComponentInChildren<Movement>().SetVelocity(damageVelocity,angle,-Movement.facingDirection);
        }
    }
}
