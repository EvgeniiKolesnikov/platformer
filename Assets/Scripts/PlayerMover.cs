using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _dashSpeed = 12f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Dash(Vector2 direction)
    {
        _rigidbody.velocity = direction * _dashSpeed;
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * _moveSpeed;
    }
}
