using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetSpeedX(float speedX)
    {
        _animator.SetFloat(Constants.AnimatorData.SpeedX, Mathf.Abs(speedX));
    }
    public void SetSpeedY(float speedY)
    {
        _animator.SetFloat(Constants.AnimatorData.SpeedY, speedY);
    }
    public void SetIdle(bool idle)
    {
        _animator.SetBool(Constants.AnimatorData.Idle, idle);
    }
    public void SetAttackTrigger()
    {
        _animator.SetTrigger(Constants.AnimatorData.IsAttack);
    }
}
