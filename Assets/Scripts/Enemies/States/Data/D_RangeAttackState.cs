using UnityEngine;

namespace Enemies.States.Data
{
   [CreateAssetMenu(fileName = "newRangeAttackStateData",menuName="Data/State Data/RangeAttack State")]
   public class D_RangeAttackState : ScriptableObject
   {
      public GameObject projectile;
      public float projectileDamage = 5f;
      public float projectileSpeed = 12f;
      public float projectileTravelDistance =5f;
   }
}
