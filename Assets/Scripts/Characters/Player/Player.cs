using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(Dash))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler), typeof(Fliper))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private Mover _mover;
    private PlayerAnimator _playerAnimator;
    private CollisionHandler _collisionHandler;
    private Fliper _fliper;
    private Dash _dash;

    private IInteractable _interactable;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _fliper = GetComponent<Fliper>();
        _dash = GetComponent<Dash>();
        transform.Flip();
    }

    private void OnEnable()
    {
        _collisionHandler.FinishReached += OnFinishReached;
    }

    private void OnDisable()
    {
        _collisionHandler.FinishReached -= OnFinishReached;
    }

    private void FixedUpdate()
    {
        UpdateDashStatus();
        UpdatePlayerAnimator();
        PlayerMove();
        PlayerInteract();

    }

    private void PlayerInteract()
    {
        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    private void OnFinishReached(IInteractable interactable)
    {
        _interactable = interactable;
    }

    private void UpdateDashStatus()
    {
        if (_inputReader.GetIsDash() && _dash.IsCanDash())
        {
            _dash.SetDirection(_inputReader.Direction);
            _dash.StartDash();
        }
    }

    private void UpdatePlayerAnimator()
    {
        _playerAnimator.SetSpeedX(_inputReader.Direction.x);
        _playerAnimator.SetSpeedY(_inputReader.Direction.y);

        if (_inputReader.Direction.x == 0 && _inputReader.Direction.y == 0)
            _playerAnimator.SetIdle(true);
        else
            _playerAnimator.SetIdle(false);
    }

    private void PlayerMove()
    {
        if (_inputReader.Direction != Vector2.zero)
        {
            _fliper.LookAtTarget(transform.position + Vector3.right * _inputReader.Direction.x);
        }
        // if ((direction.x > 0 && _isTurnLeft)
        // || (direction.x < 0 && _isTurnLeft == false))
        // {
        //     _isTurnLeft = !_isTurnLeft;
        //     transform.Flip();
        // }
        if (_dash.IsDashing)
            _mover.Move(_dash.DashDirection, _dash.DashSpeed);
        else
            _mover.Move(_inputReader.Direction, _mover.MoveSpeed);
    }
}
