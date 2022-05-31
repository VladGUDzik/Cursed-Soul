using UnityEngine;

namespace Enemies.States.Data
{
   [CreateAssetMenu(fileName = "newLoolForHeroStateData",menuName="Data/State Data/LookForHero State")]
   public class D_LookForHero : ScriptableObject
   {
      public int amountOfTurns = 2;

      public float timeBetweenTurns = 0.75f;
   }
}
