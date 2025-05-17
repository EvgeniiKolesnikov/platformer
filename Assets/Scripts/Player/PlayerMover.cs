using UnityEditor.Tilemaps;
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
        Flip();
    }

    public void Move(Vector2 direction, float speed)
    {
        _rigidbody.velocity = direction * speed;

        if ((direction.x > 0 && _isTurnLeft)
        || (direction.x < 0 && _isTurnLeft == false))
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isTurnLeft = !_isTurnLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
