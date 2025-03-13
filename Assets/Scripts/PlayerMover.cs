using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour {
    private Rigidbody2D _rb;

    [SerializeField] float _speed = 1;
    [SerializeField] float _dashForce = 2000;
    private const float SPEED_COEFF = 50;
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";
    private Vector3 _direction;
    private bool _isDash;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        _direction = new Vector2(Input.GetAxis(HORIZONTAL_AXIS), Input.GetAxis(VERTICAL_AXIS));

        if (_direction != Vector3.zero) {
            transform.up = -_direction.normalized;
        }
        if (!_isDash && Input.GetKeyDown(KeyCode.Space)) {
            _isDash = true;
        }
    }

    void FixedUpdate() {
        _rb.velocity = _speed * SPEED_COEFF * Time.fixedDeltaTime * _direction;
        if (_isDash) {
            _rb.AddForce(_direction * _dashForce);
            _isDash = false;
        }
    }
}
