using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _dashSpeed = 12f;

    private float _dashTimer = 0f;
    private float _dashDuration = 0.25f;
    private float _dashReloadTime = 3f;
    private bool _isDashing;

    private Rigidbody2D _rigidbody;

    private Vector3 _direction;
    private float _directionX;
    private float _directionY;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_isDashing)
        {
            _directionX = Input.GetAxis(HORIZONTAL_AXIS);
            _directionY = Input.GetAxis(VERTICAL_AXIS);
            _direction = new Vector2(_directionX, _directionY).normalized;

            if (Input.GetKeyDown(KeyCode.Space) && CanDash())
            {
                _dashTimer = 0f;
                _isDashing = true;
            }
        }
    }

    private void FixedUpdate()
    {
        _dashTimer += Time.fixedDeltaTime;

        if (_isDashing)
        {
            Dash();
        }
        else
        {
            Move();
        }
    }

    private bool CanDash()
    {
        return _dashTimer >= _dashReloadTime;
    }

    private void Dash()
    {
        _rigidbody.velocity = _direction * _dashSpeed;
        if (_dashTimer >= _dashDuration)
        {
            _isDashing = false;
        }
    }

    private void Move()
    {
        _rigidbody.velocity = _direction * _moveSpeed;
    }
}
