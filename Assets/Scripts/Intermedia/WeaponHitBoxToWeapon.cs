using UnityEngine;
using Weapons;

namespace Intermedia
{
   public class WeaponHitBoxToWeapon : MonoBehaviour
   {
      private AggressiveWeapon _weapon;

      private void Awake()
      {
         _weapon = GetComponentInParent<AggressiveWeapon>();
      }
   
      private void OnTriggerEnter2D(Collider2D col)
      {
         _weapon.AddTodetected(col);
      }

      private void OnTriggerExit2D(Collider2D col)
      {
         _weapon.RemoveFromDetected(col);
      }
   }
}
