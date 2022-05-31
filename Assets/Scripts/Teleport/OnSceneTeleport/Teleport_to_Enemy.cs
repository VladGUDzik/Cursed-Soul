using UnityEngine;

namespace Teleport.OnSceneTeleport
{
    public class Teleport_to_Enemy : MonoBehaviour
    {
        [SerializeField]private Transform teleportPoint;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = teleportPoint.transform.position;
            }
        }
    }
}
