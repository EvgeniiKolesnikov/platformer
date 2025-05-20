using UnityEngine;

public class AttackStateBehavior : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.transform.root.TryGetComponent(out CharacterAttacker characterAttacker))
        {
            characterAttacker.StopAttack();
        }
    }
}
