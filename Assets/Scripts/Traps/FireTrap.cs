using System.Collections;
using Audio;
using Health.HeroDeath;
using UnityEngine;

namespace Traps
{
  public class FireTrap : MonoBehaviour
  {
    [SerializeField] private float amountDamage;
    
    [Header("Firetrap Timer")] [SerializeField]
    private float activationDelay;
    [SerializeField]private float activeTime;

    [Header("SFX")] [SerializeField] private AudioClip fireSound;
    
    private Animator _anim;
    private SpriteRenderer _spriteRend;

    private bool _triggered;
    private bool _active;
  
    private void Awake()
    {
      _anim = GetComponent<Animator>();
      _spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
      if (!other.CompareTag("Player")) return;
      if (!_triggered)
        StartCoroutine(ActivateFiretrap());
      if(_active)
        other.GetComponent<DeathHero>().Damage(amountDamage);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      if (other.CompareTag("Player"))
      {
        _active =false;
      }
    }

    private IEnumerator ActivateFiretrap()
    {
      _triggered = true;
      _spriteRend.color = Color.red;
    
      yield return new WaitForSeconds(activationDelay);
      SoundManager.instance.PlaySound(fireSound);
      _spriteRend.color = Color.white;
      _active = true;
      _anim.SetBool("activated",true);
    
      yield return new WaitForSeconds(activeTime);
      _active = false;
      _triggered = false;
      _anim.SetBool("activated", false);
    }
  }
}
