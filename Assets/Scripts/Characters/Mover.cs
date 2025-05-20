using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    public float MoveSpeed { get; private set; } = 2f;
    public float EnemySpeed { get; private set; } = 0.03f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction, float speed)
    {
        _rigidbody.velocity = direction * speed;
    }

    public void Move(Transform target, float speed)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, speed);
        _rigidbody.MovePosition(newPosition);
    }
}
