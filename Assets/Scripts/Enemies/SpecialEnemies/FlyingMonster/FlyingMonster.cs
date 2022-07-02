using Audio;
using Core.CoreComponents;
using Health.HeroDeath;
using Pathfinding;
using UnityEngine;

namespace Enemies.SpecialEnemies.FlyingMonster
{
   public class FlyingMonster : MonoBehaviour
   {

      [SerializeField] private AIPath aiPath;
      [SerializeField] protected  AudioClip meleeAttackSound;
      [SerializeField] private int damageAmount;
      [SerializeField] private Vector2 angle;
      
      private SpriteRenderer _sprite;
      
      
      public void Awake()
      {
         _sprite = GetComponent<SpriteRenderer>();
      }

      private void OnTriggerEnter2D(Collider2D col)
      {
         if (!col.CompareTag("Player")) return;
         
         col.GetComponent<DeathHero>().Damage(damageAmount);
         SoundManager.instance.PlaySound(meleeAttackSound);
         col.GetComponentInChildren<Movement>().SetVelocity(10f, angle, -1);
      }

      public void Update()
      {
         _sprite.flipX = aiPath.desiredVelocity.x <= 0.01f;
      }
   }
}
