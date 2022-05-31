using System;
using Audio;
using Core.CoreComponents;
using Interfaces;
using UnityEngine;

namespace Traps
{
    public class SpikeHead : DefaultSpikes.DefaultSpikes,IDamageable
    {
        [Header("SpikeHead Attributes")]
        [SerializeField] private float speed;
        [SerializeField] private float rangeHit;
        [SerializeField] private float checkDelay;
    
        [SerializeField] private LayerMask whatIsHero;
        [SerializeField] private LayerMask whatIsGround;

        private float _checkTimer;
        private const float _groundRange = 1f;

        private Vector3 _destination;
        private Vector3[] _directions = new Vector3[4];
    
        private bool _attacking;

        [Header("SFX")] [SerializeField] private AudioClip impactSound;

        private void OnEnable()
        {
            Stop();
        }

        private void CheckAttacking()
        {
            if(_attacking)
                transform.Translate(_destination*Time.deltaTime *speed);
            else
            {
                _checkTimer += Time.deltaTime;
                if (_checkTimer > checkDelay)
                    CheckForPlayer();
            }
        }

        private void Update()
        {
            CheckAttacking();
        }

        private void CheckForPlayer()
        {
            CalculateDirections();

            foreach (var dir in _directions)
            {
                var position = transform.position;
                Debug.DrawRay(position,dir,Color.red);
                var hit = Physics2D.Raycast(position, dir,rangeHit,whatIsHero);
                var check = Physics2D.Raycast(position, dir,_groundRange,whatIsGround);
                if (hit.collider!=null && !_attacking &&!check/*.collider==null*/)
                {
                    _attacking = true;
                    _destination = dir;
                
                    _checkTimer = 0;
                }
            }
        }

        private void CalculateDirections()
        {
            var right = transform.right;
            var up = transform.up;
            _directions[0] = right * rangeHit;
            _directions[1] = -right * rangeHit;
            _directions[2] = up * rangeHit;
            _directions[3] = -up * rangeHit;
        }

        private void Stop()
        {
            _destination = Vector3.zero;;
            _attacking = false;
        }
    
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            SoundManager.instance.PlaySound(impactSound);
            base.OnTriggerEnter2D(other);
        
            Stop();
        }

        public void Damage(float amount)//
        {
            throw new NotImplementedException();
        }
        
    }
}
