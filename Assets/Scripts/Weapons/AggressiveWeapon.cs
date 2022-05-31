using System.Collections.Generic;
using System.Linq;
using Audio;
using Core.CoreComponents;
using Interfaces;
using ScriptbleObjects.Weapons;
using UnityEngine;

namespace Weapons
{
   public class AggressiveWeapon : Weapon
   {
      private Movement Movement => _movement ??= Core.GetCoreComponent(ref _movement);
      private Movement _movement;
      
      protected SO_AggressiveWeaponData AggressiveWeaponData;
      private List<IDamageable> _detectedDamageables = new List<IDamageable>();
      private List<IKnockbackable> _detectedKnockbackables = new List<IKnockbackable>();
      [SerializeField] private AudioClip attackSound;
      protected override void Awake()
      {
         base.Awake();

         if (weaponData.GetType()==typeof(SO_AggressiveWeaponData))
         {
            AggressiveWeaponData = (SO_AggressiveWeaponData) weaponData;
         }
         else
         {
            Debug.LogError("Wrong data for the weapon");
         }
      }

      public override void AnimationActionTrigger()
      {
         base.AnimationActionTrigger();
         SoundManager.instance.PlaySound(attackSound);
         CheckMeleeAttack();
      }

      private void CheckMeleeAttack()
      {
         var details = AggressiveWeaponData.AttackDetails[AttackCounter];
      
         foreach (var item in _detectedDamageables.ToList())
         {
            item.Damage(details.damageAmount);
         }

         foreach (var item in _detectedKnockbackables.ToList())
         {
            item.Knockback(details.knockbackAngle,details.knockbackStrength,Movement.facingDirection);
         }
      }

      public void AddTodetected(Collider2D col)
      {
         var damageable = col.GetComponent<IDamageable>();
         if (damageable != null)
         {
            _detectedDamageables.Add(damageable);
         }

         var knockable = col.GetComponent<IKnockbackable>();
         if (knockable != null)
         {
            _detectedKnockbackables.Add(knockable);
         }
      }

      public void RemoveFromDetected(Collider2D col)
      {
         var damageable = col.GetComponent<IDamageable>();
         if (damageable != null)
         {
            _detectedDamageables.Remove(damageable);
         }
      
         var knockable = col.GetComponent<IKnockbackable>();
         if (knockable != null)
         {
            _detectedKnockbackables.Remove(knockable);
         }
      }
   }
}
