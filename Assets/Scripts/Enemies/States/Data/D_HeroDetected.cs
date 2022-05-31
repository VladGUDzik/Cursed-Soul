using UnityEngine;

namespace Enemies.States.Data
{
     [CreateAssetMenu(fileName = "newHeroDetectedStateData",menuName = "Data/State Data/HeroDetected Data")]
     public class D_HeroDetected : ScriptableObject
     {
          public float longRangeActionTime=1.5f;
     }
}
