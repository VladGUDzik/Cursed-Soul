using System;
using Core.CoreComponents;
using Health.HeroDeath;
using Health.MainHeart;
using UnityEngine;
using UnityEngine.Serialization;

namespace Traps.DefaultSpikes
{
    public class DefaultSpikes : MonoBehaviour
    {
        [SerializeField] protected float damageSpikes;
        [SerializeField] private Vector2 angle;
        [SerializeField] public bool poison;
        public float startPeriod;
        private float period;
        [SerializeField] protected float damagePoison;
        private bool checkPoison;

        private void Start()
        {
            checkPoison = false;
        }

        private void FixedUpdate()
        {
            if (checkPoison)
            {
                period -= Time.deltaTime;
                if ( period > 0 )
                {
                   HealthComponent.instanceHealthComponent.SetBonusHealth(HealthComponent.instanceHealthComponent.currentHealth - damagePoison);
                }
                else
                {
                    checkPoison = false;
                }
                
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (poison)
                {
                    period = startPeriod;
                    checkPoison = true;
                    other.GetComponent<DeathHero>().Damage(damageSpikes);
                    other.GetComponentInChildren<Movement>().SetVelocity(10f, angle, -1);
                }
                else
                {
                    other.GetComponent<DeathHero>().Damage(damageSpikes);
                    other.GetComponentInChildren<Movement>().SetVelocity(10f, angle, -1);
                }
                
            }
            
        }
    }
}