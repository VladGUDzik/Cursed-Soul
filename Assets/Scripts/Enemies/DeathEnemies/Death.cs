using Audio;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.DeathEnemies
{
    public class Death : MonoBehaviour,IDamageable
    {
        [SerializeField] protected GameObject
            deathChunkParticle,
            deathBloodParticle,
            hitParticles;

        [SerializeField] protected Material matBlink;
        [SerializeField] private float maxHealth;
        [SerializeField] protected AudioClip deathSound,hurtSound;
        
        protected Material MatDefault;
        protected SpriteRenderer SpriteRend;
        public float currentHealth { get; private set; }
        private float _startSound;
        private void Start()
        {
            currentHealth = maxHealth;
            SpriteRend = GetComponent<SpriteRenderer>();
            
            MatDefault = SpriteRend.material;
        }

        private void ResetMaterial()
        {
            SpriteRend.material = MatDefault;
        }

        protected void OverDestroyer(float health)
        {
            if(health <= 0)
            {
                var position = transform.position;
                Instantiate(deathChunkParticle, position, deathChunkParticle.transform.rotation);
                Instantiate(deathBloodParticle, position, deathBloodParticle.transform.rotation);

                GameObject o;
                (o = gameObject).SetActive(false);
                Destroy(o);
                SoundManager.instance.PlaySound(deathSound);
            }
            else
            {
                if (Time.time >= _startSound + hurtSound.length)
                {
                    SoundManager.instance.PlaySound(hurtSound);
                    _startSound = Time.time;
                }
                Invoke(nameof(ResetMaterial), .4f);
            }
        }
        
        protected void BlinkMaterial()
        {
            SpriteRend.material = matBlink;
        }

        public virtual void Damage(float amount)
        {
            currentHealth -= amount;
            Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        
            BlinkMaterial();
            OverDestroyer(currentHealth);
        
        }
    }
}
