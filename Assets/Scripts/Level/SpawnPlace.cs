using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpawnPlace : MonoBehaviour
{
    private Animator _animator;
    public bool IsActive { get; private set; } = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Deactivate()
    {
        IsActive = false;
        _animator.SetTrigger(Constants.AnimatorData.IsOff);
    }
}
