using System;
using Audio;
using Core.CoreComponents;
using Health.HeroDeath;
// ReSharper disable once RedundantUsingDirective
using Pathfinding;
using UnityEngine;

namespace Enemies.SpecialEnemies.FlyingMonster
{
   public class FlyingMonster : MonoBehaviour
   {
      [SerializeField]private Core.Core core;
      protected Movement Movement => _movement ??= core.GetCoreComponent(ref _movement);
      private Movement _movement;
      
      [SerializeField] private AIPath aiPath;
      [SerializeField] protected  AudioClip meleeAttackSound;
      [SerializeField] private int damageAmount;
      [SerializeField] private Vector2 angle;
      private Animator _animator;
      
      private SpriteRenderer _sprite;

      public void Awake()
      {
         _sprite = GetComponent<SpriteRenderer>();
         _animator = GetComponent<Animator>();
      }

      private void OnTriggerEnter2D(Collider2D col)
      {
         if (!col.CompareTag("Player")) return;
         
         col.GetComponent<DeathHero>().Damage(damageAmount);
         SoundManager.instance.PlaySound(meleeAttackSound);
         col.GetComponentInChildren<Movement>().SetVelocity(10f, angle,Movement.facingDirection*-3);
         
         _animator.SetBool("attack",true);
         _animator.SetBool("move",false);
      }
      
      private void OnTriggerExit2D(Collider2D other)
      {
         _animator.SetBool("move",true);
         if (!other.CompareTag("Player")) 
            _animator.SetBool("attack",false);
      }

      public void Update()
      {
         _sprite.flipX = aiPath.desiredVelocity.x <= 0.01f;
      }
   }
}
