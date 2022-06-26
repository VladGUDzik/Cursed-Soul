using System;
using Enemies.DeathEnemies;
using Health.MainHeart;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Health.HeroDeath
{
    public class DeathHero : Death
    {
        public HealthComponent healComponent;
        public static DeathHero InstanceDeathHero { get; private set; }
        [SerializeField] private StatusFace statusFace;
        private void Start()
        {
            healComponent.SetMaxHealth(healComponent.maxHealth);
            SpriteRend = GetComponent<SpriteRenderer>();
           // maxHealth = healComponent.MaxHealth;
            MatDefault = SpriteRend.material;
        }
        
        public override void Damage(float amount)
        {
            healComponent.currentHealth -= amount;
            Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

            statusFace.HealthChange(healComponent.currentHealth);
            
            BlinkMaterial();
            healComponent.SetHealth(healComponent.currentHealth);
            OverDestroyer(healComponent.currentHealth);
            
        }

        public static void Poison(float amount, float period, ref bool checkPoison)
        {
            

        }
    }
}
