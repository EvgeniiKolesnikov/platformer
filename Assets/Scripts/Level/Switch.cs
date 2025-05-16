using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Switch : MonoBehaviour, IInteractable
{
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
            _animator.SetTrigger(Constants.AnimatorData.IsOn);
        else
            _animator.SetTrigger(Constants.AnimatorData.IsOff);
    }
}
