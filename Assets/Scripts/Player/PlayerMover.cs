using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    public float MoveSpeed { get; private set; } = 2f;

    private Rigidbody2D _rigidbody;
    private bool _isTurnLeft = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _isTurnLeft = false;
        transform.Flip();
    }

    public void Move(Vector2 direction, float speed)
    {
        _rigidbody.velocity = direction * speed;

        if ((direction.x > 0 && _isTurnLeft)
        || (direction.x < 0 && _isTurnLeft == false))
        {
            _isTurnLeft = !_isTurnLeft;
            transform.Flip();
        }
    }

}
