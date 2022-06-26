using Core.CoreComponents;
using UnityEngine;
using Health.MainHeart;
public class AllBonuses : MonoBehaviour
{
    public float hp;
    public float speed;
    public float damage;
    public float shield;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (hp != 0)
                HealthComponent.instanceHealthComponent.SetBonusHealth(HealthComponent.instanceHealthComponent.currentHealth + hp);
           // if (speed!=0)
            //Movement.instanceMovement.SetVelocity();
                


        }
    }

}
