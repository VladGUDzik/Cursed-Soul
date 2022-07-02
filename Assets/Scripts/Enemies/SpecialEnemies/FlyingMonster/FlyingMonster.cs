using Enemies.State_Machine;
using Pathfinding;
using UnityEngine;

namespace Enemies.SpecialEnemies.FlyingMonster
{
   public class FlyingMonster : Entity
   {
      [SerializeField] private AIPath aiPath;
      private SpriteRenderer _sprite;

      public override void Awake()
      {
         _sprite = GetComponent<SpriteRenderer>();
      }

      public override void Update()
      {
         _sprite.flipX = aiPath.desiredVelocity.x <= 0.01f;
      }
   }
}
