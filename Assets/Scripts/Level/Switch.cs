using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField] private ActivatableObject _activatable;

    private Animator _animator;
    public bool IsActive { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        IsActive = !IsActive;

        if (IsActive)
        {
            _animator.SetTrigger(Constants.AnimatorData.IsOn);
            _activatable?.Activate();
        }
        else
        {
            _animator.SetTrigger(Constants.AnimatorData.IsOff);
            _activatable?.Deactivate();
        }
    }
}
