using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _speed = 0.03f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private Vector2 _seeAreaSize;
    [SerializeField] private LayerMask _targetLayer;

    private Rigidbody2D _rigidbody;
    private bool _isTurnLeft = true;
    private int _wayPointIndex;
    private Transform _target;
    private float _maxSqrDistance = 0.02f;
    private bool _isWaiting = false;
    private float _endWaitTime;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapBox(GetLookOrigin(), _seeAreaSize, 0, _targetLayer);

        if (hit != null)
        {
            // print(hit.gameObject.name);
        }

        if (_isWaiting == false)
        {
            Move();
        }

        if (IsTargetReached() && _isWaiting == false)
        {
            _isWaiting = true;
            _endWaitTime = Time.time + _waitTime;
        }
        if (_isWaiting && _endWaitTime <= Time.time)
        {
            ChangeTarget();
            _isWaiting = false;
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
        _isTurnLeft = !_isTurnLeft;
        transform.Flip();
    }

    private Vector2 GetLookOrigin()
    {
        float halfCoeff = 4f;
        int directionCoeff = _isTurnLeft ? -1 : 1;
        float originX = transform.position.x + _seeAreaSize.x / halfCoeff * directionCoeff;
        float originY = transform.position.y;
        return new Vector2(originX, originY);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetLookOrigin(), _seeAreaSize);
    }
}
