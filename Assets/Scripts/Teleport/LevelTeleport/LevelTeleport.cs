using UnityEngine;
using UnityEngine.SceneManagement;

namespace Teleport.LevelTeleport
{
    public class LevelTeleport : MonoBehaviour
    {
        public int Scene;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(Scene);
            }
        }
    }
}

