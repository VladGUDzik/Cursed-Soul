using UnityEngine;

namespace Hero.Data
{
   [CreateAssetMenu (fileName = "newHeroData",menuName = "Data/Hero Data/Base Data")]

   public class HeroData : ScriptableObject
   {
      [Header("Move State")] public float movementVelocity = 10f;

      [Header("Jump State")] public float jumpVelocity = 15f;
      public int amountOfJumps = 1;
   
      [Header("Wall Jump State")] public float wallJumpVelocity = 20f;
      public float wallJumpTime = .4f;
      public Vector2 wallJumpAngle = new Vector2(1, 2);
   
      [Header("In Air State")] public float coyoteTime = .2f;
      public float variableJumpHeightMultiplier = .5f;

      [Header("Wall Slide State")] 
      public float wallSlideVelocity = 3f;
   
      [Header("Wall Climb State")] 
      public float wallClimbVelocity = 3f;

      [Header("Ledge Climb State")] 
      public Vector2 startOffset;
      public Vector2 stopOffset;

      [Header("Dash State")] 
      public float dashCoolDown = .5f;
      public float maxHoldTime = 1f;
      public float holdTimeScale = .25f;
      public float dashTime = .2f;
      public float dashVelocity = 30f;
      public float drag = 10f;
      public float dashEndYMultiplier =.2f ;
      public float distBetweenAfterImages = .5f;

      [Header("Sound")] 
      [SerializeField] public AudioClip attackSound;

   }
}