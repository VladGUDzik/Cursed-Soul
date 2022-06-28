using Enemies.DeathEnemies;
using Health.HeroDeath;
using UnityEngine;

namespace Traps
{
   public class TrapProjectile : MonoBehaviour
   {
      [SerializeField] protected GameObject hitParticles;
   
      [SerializeField] private float speed;
      [SerializeField] private float resetTime;
      [SerializeField] private float damage;
      
      private float _lifeTime;

      public void LogicInFly()
      {
         _lifeTime = 0;
         gameObject.SetActive(true);
      }

      private void Update()
      {
         var moveSpeed = speed * Time.deltaTime;
         transform.Translate(moveSpeed,0,0);

         _lifeTime += Time.deltaTime;
         if(_lifeTime>resetTime)
            gameObject.SetActive(false);
      }

      private void OnTriggerEnter2D(Collider2D other)
      {
         if (other.CompareTag("Player")) 
            other.GetComponent<DeathHero>().Damage(damage);
         else if(other.CompareTag("Enemies"))
            other.GetComponent<Death>().Damage(damage);//
      
         Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
      
         gameObject.SetActive(false);
      }

   }
}
