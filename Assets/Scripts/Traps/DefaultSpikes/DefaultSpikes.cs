using System;
using Core.CoreComponents;
using Health.HeroDeath;
using UnityEngine;

namespace Traps.DefaultSpikes
{
    public class DefaultSpikes : MonoBehaviour
    {
        [SerializeField] protected float damageSpikes;
        [SerializeField] private Vector2 angle;
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            other.GetComponent<DeathHero>().Damage(damageSpikes);
            other.GetComponentInChildren<Movement>().SetVelocity(10f,angle,-1);

        }
    }
}