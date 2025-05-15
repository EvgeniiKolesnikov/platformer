using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover), typeof(Dash))]

public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _playerMover;
    private Dash _dash;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>();
        _dash = GetComponent<Dash>();
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
        if (_dash.IsDashing)
        {
            _playerMover.Dash(_dash.DashDirection);
        }
        else
        {
            _playerMover.Move(_inputReader.Direction);
        }
    }
}
