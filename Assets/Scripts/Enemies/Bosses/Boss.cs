using System.Collections.Generic;
using Core.CoreComponents;
using Enemies.Bosses.Data;
using Enemies.DeathEnemies;
using Interfaces;
using UnityEngine;

namespace Enemies.Bosses
{
    public class Boss : Death
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform attackPosition;
        [SerializeField]private D_BossAttack dBossAttack;

        private bool _isFlipeed;

        public bool isInvinceble;
       
        public Core.Core core { get; private set; }

        public Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
        private Movement _movement;
        

        public Combat combat => _combat ??= core.GetCoreComponent(ref _combat);
        private Combat _combat;
        
        private void Awake()
        {
            core = GetComponentInChildren<Core.Core>();
        }
        
        public void LookAtPlayer()
        {
            if(transform.position.x < player.position.x && _isFlipeed)
            {
               Movement.Flip();
               _isFlipeed = false;
            }
            else if(transform.position.x > player.position.x && !_isFlipeed)
            {
               Movement.Flip();
               _isFlipeed = true;
            }
        }

        public override void Damage(float amount)
        {
            if (isInvinceble) return;
            base.Damage(amount);
        }

        public void TriggerAttack()
        {
            var detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position,dBossAttack.attackRadius,
                dBossAttack.whatIsHero);
            
            
            foreach (var collider in detectedObjects)
            {
                var damageable = collider.GetComponent<IDamageable>();
                damageable?.Damage(dBossAttack.attackDamage);
            
                var knockbackable = collider.GetComponent<IKnockbackable>();
                knockbackable?.Knockback(dBossAttack.knockbackAngle, dBossAttack.knockbackStrength,
                    Movement.facingDirection);
            }
        }
    }
}