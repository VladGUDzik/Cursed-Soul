using UnityEngine;

namespace Traps
{
    public class ArrowTrap : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject[] projectiles;

        private float _cooldownTimer;
        
        private void Attack()
        {
            _cooldownTimer = 0;
            projectiles[FindProjectile()].transform.position = firePoint.position;
            projectiles[FindProjectile()].GetComponent<TrapProjectile>().LogicInFly();
        }

        private int FindProjectile()
        {
            for (var i = 0; i < projectiles.Length; i++)
            {
                if (!projectiles[i].activeInHierarchy) return i;
            }
            return 0;
        }

        private void Update()
        {
            _cooldownTimer += Time.deltaTime;
            
            if(_cooldownTimer>=attackCooldown)
                Attack();
        }
    }
}
