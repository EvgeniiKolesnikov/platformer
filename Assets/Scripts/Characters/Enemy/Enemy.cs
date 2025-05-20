using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Fliper), typeof(EnemyVision))]
[RequireComponent(typeof(CharacterAnimator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _waitTime = 2f;

    private Mover _mover;
    private Fliper _fliper;
    private EnemyVision _enemyVision;
    private CharacterAnimator _characterAnimator;
    private Vector2 _enemyDirection;
    private Transform _target;

    private int _wayPointIndex;
    private float _maxSqrDistance = 0.02f;
    private float _endWaitTime;

    private bool _isWaiting = false;
    private bool _isFollowing = false;
    // private bool _isSleep = false;

    private void Start()
    {
        _mover = GetComponent<Mover>();
        _fliper = GetComponent<Fliper>();
        _enemyVision = GetComponent<EnemyVision>();
        _characterAnimator = GetComponent<CharacterAnimator>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        _enemyDirection = _target.position - transform.position;

        if (_enemyVision.TrySeeTarget(out Transform target, _enemyDirection))
        {
            _isFollowing = true;
            _mover.Move(target, _mover.EnemySpeed);
            _isWaiting = false;
            _target = target;
            _fliper.LookAtTarget(_target.position);
            UpdateCharacterAnimator();
            return;
        }
        else
        {
            if (_isFollowing)
            {
                _isFollowing = false;
                _target = _wayPoints[_wayPointIndex].transform;
                _fliper.LookAtTarget(_target.position);
            }
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
            _isWaiting = false;
            ChangeTarget();
        }

        UpdateCharacterAnimator();
    }

    private void UpdateCharacterAnimator()
    {
        if (_isWaiting)
        {
            _enemyDirection = Vector2.zero;
            _characterAnimator.SetIdle(true);
        }
        else
        {
            _characterAnimator.SetIdle(false);

        }
        _characterAnimator.SetSpeedX(_enemyDirection.x);
        _characterAnimator.SetSpeedY(_enemyDirection.y);

        // print("_enemyDirection = " + _enemyDirection);
        // print("transform.position.x = " + transform.position.x + ", transform.position.y = " + transform.position.y);
        // print("_target.position.x = " + _target.position.x + ", _target.position.y = " + _target.position.y);
        // print("_enemyDirection.x = " + _enemyDirection.x + ", _enemyDirection.y = " + _enemyDirection.y);
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
