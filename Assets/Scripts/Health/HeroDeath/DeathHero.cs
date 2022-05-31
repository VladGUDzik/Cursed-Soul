using Enemies.DeathEnemies;
using Health.MainHeart;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Health.HeroDeath
{
    public class DeathHero : Death
    {
        public HealthComponent healComponent;
        [SerializeField] private StatusFace statusFace;
        private void Start()
        {
            healComponent.SetMaxHealth(healComponent.MaxHealth);
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
    }
}
