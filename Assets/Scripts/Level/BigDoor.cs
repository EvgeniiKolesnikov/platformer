using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BigDoor : ActivatableObject, IActivatable
{
    [SerializeField] private Switch[] _switches;
    private Animator _animator;

    public bool IsActive { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Activate()
    {
        if (_switches.All(i => i.IsActive))
        {
            IsActive = true;
            _animator.SetTrigger(Constants.AnimatorData.IsOn);
        }
    }
    public override void Deactivate()
    {
        if (IsActive)
        {
            IsActive = false;
            _animator.SetTrigger(Constants.AnimatorData.IsOff);
        }
    }
}
