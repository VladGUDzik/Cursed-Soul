using Enemies.Bosses;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    [SerializeField] private float speed = 2.5f,
        attackRange;

    private Transform _player;

    private Rigidbody2D _rb;

    private Boss _boss;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player??= FindObjectOfType<Hero.HeroFiniteStateMachine.Hero>().transform;
        _rb=animator.GetComponent<Rigidbody2D>();
        _boss = animator.GetComponent<Boss>();
    }
    
   public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player == null)
        {
            animator.SetTrigger("Idle");
            animator.SetBool("IsEnraged",false);
            return;
        }
        if (_boss.currentHealth <= 100)
        {
            animator.SetBool("IsEnraged",true);
        }
        
        CheckPlayer();

        if (Vector2.Distance(_player.position, _rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
            animator.SetFloat("playerPositionY",_player.position.y);
        }
    }

    private void CheckPlayer()
    {
        _boss.LookAtPlayer();
        
        var target = new Vector2(_player.position.x, _rb.position.y);

        var newPos = Vector2.MoveTowards(_rb.position, target, speed *Time.fixedDeltaTime);
        _rb.MovePosition(newPos);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}
