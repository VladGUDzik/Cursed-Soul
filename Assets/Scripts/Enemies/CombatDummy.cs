using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class CombatDummy : MonoBehaviour, IDamageable,IKnockbackable
    {
        [SerializeField] private GameObject hitParticles;
        [SerializeField] private float health;
        private Animator _anim;
        private Vector2 _knockbackVelocity;
        [SerializeField] private GameObject 
            deathChunkParticle,
            deathBloodParticle;

        private static readonly int DamageCashName = Animator.StringToHash("damage");

        public void Damage(float amount)
        {
            Debug.Log(amount + " ApplyStun taken");
            DecreaseHealth(amount);
        
            Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            _anim.SetTrigger(DamageCashName);
            if (health >= 0) return;
            var position = transform.position;
            Instantiate(deathChunkParticle, position, deathChunkParticle.transform.rotation);
            Instantiate(deathBloodParticle, position, deathBloodParticle.transform.rotation);

            GameObject o;
            (o = gameObject).SetActive(false);
            Destroy(o);
        }
    
        private void DecreaseHealth(float damage)
        {
            health -= damage;
        }

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        public void Knockback(Vector2 angle, float strength, int direction)
        {
            angle.Normalize();
            _knockbackVelocity.Set(angle.x*strength*direction,angle.y * strength);
        }
    }
}