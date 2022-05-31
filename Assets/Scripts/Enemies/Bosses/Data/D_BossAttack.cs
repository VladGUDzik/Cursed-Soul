using UnityEngine;

namespace Enemies.Bosses.Data
{ 
    [CreateAssetMenu(fileName = "newBossData",menuName="Data/Boss Data/MeleeAttack Data")]

    public class D_BossAttack : ScriptableObject
    {
        public float attackRadius = 1.5f;
        public float attackDamage = 50f;

        public Vector2 knockbackAngle = Vector2.one;
        public float knockbackStrength = 20f;

        public LayerMask whatIsHero;
    }
}

