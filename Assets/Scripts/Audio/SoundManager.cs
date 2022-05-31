using UnityEngine;

namespace Audio
{
   public class SoundManager : MonoBehaviour
   {
      public static SoundManager instance { get; private set; }
      public AudioSource audioSource { get; private set; }

      private void Awake()
      {
         audioSource = GetComponent<AudioSource>();

         if (instance == null)
         {
            instance = this;
            DontDestroyOnLoad(gameObject);
         }else if (instance != null && instance != this)
         {
            Destroy(gameObject);
         }
      }

      public void PlaySound(AudioClip audioClip)
      {
         audioSource.PlayOneShot(audioClip);
      }

   }
}
