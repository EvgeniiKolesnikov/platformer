using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover), typeof(Dash))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _playerMover;
    private PlayerAnimator _playerAnimator;
    private CollisionHandler _collisionHandler;
    private Dash _dash;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _dash = GetComponent<Dash>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    private void Update()
    {
        if (_inputReader.SpaceKeyPressed && _dash.IsCanDash())
        {
            _dash.SetDirection(_inputReader.Direction);
            _dash.StartDash();
        }
    }


    private void FixedUpdate()
    {
        _playerAnimator.SetSpeedX(_inputReader.Direction.x);
        _playerAnimator.SetSpeedY(_inputReader.Direction.y);

        if (_inputReader.Direction.x == 0 && _inputReader.Direction.y == 0)
        {
            _playerAnimator.SetIdle(true);
        }
        else
        {
            _playerAnimator.SetIdle(false);
        }

        if (_dash.IsDashing)
        {
            _playerMover.Move(_dash.DashDirection, _dash.DashSpeed);
        }
        else
        {
            _playerMover.Move(_inputReader.Direction, _playerMover.MoveSpeed);
        }
    }
}
