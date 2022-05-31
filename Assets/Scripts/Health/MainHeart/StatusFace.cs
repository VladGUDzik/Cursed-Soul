using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Health.MainHeart
{
   public class StatusFace : MonoBehaviour
   {
      [Header("UI Elements")] [SerializeField]
      private Image heroFace;

      [Header("Face Sprite")] [SerializeField]
      private Sprite[] faceSprites;
   
      private float _oldHealth;
      
      //private float _faceTimer;
      // private void Update()
      // {
      //    // if (_faceTimer > 0)
      //    //    _faceTimer -= Time.deltaTime;
      //    // else
      //    //    SetFace("Left Hit (54x52)"+GetDamageStatus(Random.Range(0,50)));//look
      //    
      // }
      public void HealthChange(float health)
      {
         if(health<=0)
            SetFace("Bottom Hit (54x52)_2");//Dead
         else
         {
            var damage = _oldHealth - health;
            if(damage >= 10) SetFace("Right Hit (54x52)"+GetDamageStatus(health));//damage
            else  SetFace("Blink (54x52)"+GetDamageStatus(health));//shock
         }
         _oldHealth = health;
      }
      
      private static string GetDamageStatus(float health)
      {
         var suffix = "";
         if (health >= 40) suffix = "0";
         else if (health < 40 && health >= 25) suffix = "1";
         else if (health < 25 && health >= 10) suffix = "2";
         else if (health < 10 && health >= 0) suffix = "3";
         return "_" + suffix;
      }

      private void SetFace(string face)
      {
         foreach (var faceSprite in faceSprites)
         {
            if (faceSprite.name == face)
               heroFace.sprite = faceSprite;
         }

         //_faceTimer = 1f;
      }
   }
}
