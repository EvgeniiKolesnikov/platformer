using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(Dash))]
[RequireComponent(typeof(CharacterAnimator), typeof(CollisionHandler), typeof(Fliper))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private Mover _mover;
    private CharacterAnimator _characterAnimator;
    private CollisionHandler _collisionHandler;
    private Fliper _fliper;
    private Dash _dash;

    private IInteractable _interactable;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _characterAnimator = GetComponent<CharacterAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _fliper = GetComponent<Fliper>();
        _dash = GetComponent<Dash>();
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
        UpdateCharacterAnimator();
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

    private void UpdateCharacterAnimator()
    {
        if (_inputReader.Direction.x == 0 && _inputReader.Direction.y == 0)
            _characterAnimator.SetIdle(true);
        else
            _characterAnimator.SetIdle(false);

        _characterAnimator.SetSpeedX(_inputReader.Direction.x);
        _characterAnimator.SetSpeedY(_inputReader.Direction.y);
    }

    private void PlayerMove()
    {
        if (_inputReader.Direction != Vector2.zero)
        {
            _fliper.LookAtTarget(transform.position + Vector3.right * _inputReader.Direction.x);
        }
        if (_dash.IsDashing)
            _mover.Move(_dash.DashDirection, _dash.DashSpeed);
        else
            _mover.Move(_inputReader.Direction, _mover.MoveSpeed);
    }
}
