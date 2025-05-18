using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Fliper), typeof(EnemyVision))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _waitTime = 2f;

    private Mover _mover;
    private Fliper _fliper;
    private EnemyVision _enemyVision;

    private int _wayPointIndex;
    private Transform _target;
    private float _maxSqrDistance = 0.02f;
    private bool _isWaiting = false;
    private float _endWaitTime;

    private void Start()
    {
        _mover = GetComponent<Mover>();
        _fliper = GetComponent<Fliper>();
        _enemyVision = GetComponent<EnemyVision>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        if (_enemyVision.TrySeeTarget(out Transform target))
        {
            _mover.Move(target, _mover.EnemySpeed);
            return;
        }

        if (_isWaiting == false)
        {
            _mover.Move(_target, _mover.EnemySpeed);
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

    private bool IsTargetReached()
    {
        float sqrDistance = (transform.position - _target.position).sqrMagnitude;
        return sqrDistance < _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;

        _fliper.LookAtTarget(_target.position);
    }


}
