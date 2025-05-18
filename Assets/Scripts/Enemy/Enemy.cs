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
        if (TrySeeTarget(out Transform target))
        {
            Move(target);
            return;
        }

        if (_isWaiting == false)
        {
            Move(_target);
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

    private bool TrySeeTarget(out Transform target)
    {
        target = null;

        Collider2D hit = Physics2D.OverlapBox(GetLookOrigin(), _seeAreaSize, 0, _targetLayer);

        if (hit != null)
        {
            Vector2 direction = (hit.transform.position - transform.position).normalized;
            Vector2 rayPosition = transform.position;
            float rayDistance = _seeAreaSize.x;
            LayerMask rayMask = ~(1 << gameObject.layer);

            RaycastHit2D hit2D = Physics2D.Raycast(rayPosition, direction, rayDistance, rayMask);
            if (hit2D.collider != null)
            {
                if (hit2D.collider == hit)
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.red);
                    target = hit2D.transform;
                    return true;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.white);
                }
            }
        }
        return false;
    }

    private void Move(Transform target)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, _speed);
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
