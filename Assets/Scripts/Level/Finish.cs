using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Finish : MonoBehaviour, IInteractable
{
    [SerializeField] private Switch[] _switches;
    private Animator _animator;
    public bool IsActive { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (_switches.All(i => i.IsActive))
        {
            print("F-I-N-I-S-H");
            gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        IsActive = true;
        _animator.SetTrigger(Constants.AnimatorData.IsOn);
    }
}
