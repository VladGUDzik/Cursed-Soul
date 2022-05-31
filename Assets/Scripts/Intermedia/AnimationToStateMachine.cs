using Enemies.States;
using UnityEngine;

namespace Intermedia
{
    public class AnimationToStateMachine : MonoBehaviour
    {
        public AttackState AttackState;

        private void TriggerAttack()
        {
            AttackState.TriggerAttack();
        }

        private void FinishAttack()
        {
            AttackState.FinishAttack();
        }
    }
}
