using Interfaces;
using UnityEngine;

namespace Projectile
{
    public class Projectile : MonoBehaviour,IDamageable
    {
        private float _speed;
        private float _travelDistance;
        private float _xStartPOs;
        private float _health = 1f;
        
        [SerializeField] private float gravity;
        [SerializeField] private float damageRadius;
        [SerializeField] private float damageAmount;

        private Rigidbody2D _rb;
    
        private bool _isGravityOn;
        private bool _hasHitGround;

        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private LayerMask whatIsHero;
        [SerializeField] private Transform damagePosition;
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
       
            _rb.gravityScale = 0.0f;
            var trans = transform;
            _rb.velocity = trans.right * _speed;

            _isGravityOn = false;
            _xStartPOs = trans.position.x;
        }
    
        private void GameObjectDestroy()=> Destroy(gameObject);
    
        private void FixedUpdate()
        {
            if (_hasHitGround) return;

            LogicWithHit();
        }

        private void LogicWithHit()
        {
            var position = damagePosition.position;
            var damageHit = Physics2D.OverlapCircle(position, damageRadius, whatIsHero);
            var groundHit = Physics2D.OverlapCircle(position, damageRadius, whatIsGround);

            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", damageAmount); 
                Destroy(gameObject);
            }

            if (groundHit)
            {
                _hasHitGround = true;
                _rb.gravityScale = 0f;
                _rb.velocity = Vector2.zero;
                Invoke(nameof(GameObjectDestroy),5f);
            }
        
            if (Mathf.Abs(_xStartPOs - transform.position.x) >= _travelDistance && !_isGravityOn)
            {
                _isGravityOn = true;
                _rb.gravityScale = gravity;
            }
        }

        private void LogicInFly()
        {
            if (_hasHitGround)return;
            if (_isGravityOn)
            {
                var velocity = _rb.velocity;
                var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void Update()
        {
            LogicInFly();
        }
        
        public void FireProjectile(float speed ,float travelDistance,float damage)
        {
            _speed = speed;
            _travelDistance = travelDistance;
            damageAmount = damage;
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.color=Color.red;
            Gizmos.DrawWireSphere(damagePosition.position,damageRadius);
        }
        /// <summary>
        /// Отражение стрел в процессе
        /// </summary>
        /// <param name="amount"></param>
        public void Damage(float amount)
        {
            Debug.Log("counterattack");
            DecreaseHealth(amount);
        
           //Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
           
           if (_health >= 0) return;
            // var position = transform.position;
            // Instantiate(deathChunkParticle, position, deathChunkParticle.transform.rotation);
            // Instantiate(deathBloodParticle, position, deathBloodParticle.transform.rotation);

            GameObject o;
            (o = gameObject).SetActive(false);
            Destroy(o);
        }

        private void DecreaseHealth(float amount)
        {
            _health -= amount;
        }
    }
}
