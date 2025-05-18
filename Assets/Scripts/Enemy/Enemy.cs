using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _speed = 0.03f;

    private Rigidbody2D _rigidbody;
    private bool _isTurnLeft = true;
    private int _wayPointIndex;
    private Transform _target;
    private float _maxSqrDistance = 0.05f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        Move();

        if (IsTargetReached())
        {
            ChangeTarget();
        }
    }

    private void Move()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, _target.position, _speed);
        _rigidbody.MovePosition(newPosition);
    }

    private bool IsTargetReached()
    {
        float sqrDistance = (transform.position - _target.position).sqrMagnitude;

        return sqrDistance < _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;

        if ((transform.position.x > 0 && _isTurnLeft)
        || (transform.position.x < 0 && _isTurnLeft == false))
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
