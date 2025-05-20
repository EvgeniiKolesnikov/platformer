using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    [SerializeField] private Sword _sword;

    public bool CanAttack => _sword?.IsAttack == false;
    // public bool CanAttack = true;

    public void Attack()
    {
        _sword.Attack();
    }

    public void StopAttack()
    {
        _sword.StopAttack();
    }
}
