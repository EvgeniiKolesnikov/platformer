using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Fliper), typeof(EnemyVision))]
[RequireComponent(typeof(CharacterAnimator), typeof(CharacterAttacker))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _waitTime = 2f;

    private Mover _mover;
    private Fliper _fliper;
    private EnemyVision _enemyVision;
    private CharacterAnimator _characterAnimator;
    private CharacterAttacker _characterAttacker;
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
        _characterAttacker = GetComponent<CharacterAttacker>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        _enemyDirection = _target.position - transform.position;
        _enemyDirection = _enemyDirection.normalized;
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
                InitWaiting();
                _target = _wayPoints[_wayPointIndex].transform;
            }
        }

        if (_isWaiting == false)
            _mover.Move(_target, _mover.EnemySpeed);

        if (IsTargetReached() && _isWaiting == false)
            InitWaiting();

        if (_isWaiting && _endWaitTime <= Time.time)
            ChangeTarget();

        UpdateCharacterAnimator();
    }

    private void InitWaiting()
    {
        _isWaiting = true;
        _endWaitTime = Time.time + _waitTime;
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
    }

    private bool IsTargetReached()
    {
        float sqrDistance = (transform.position - _target.position).sqrMagnitude;
        return sqrDistance < _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        _isWaiting = false;
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;
        _fliper.LookAtTarget(_target.position);
    }
}
