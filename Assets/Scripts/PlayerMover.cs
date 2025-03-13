using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] float _speedX = 1;
    [SerializeField] float _jumpForce = 500;
    private const float SPEED_COEFFICIENT = 50;
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string GROUND_TAG = "Ground";
    private float _direction;
    private bool _isJump;
    private bool _isGround;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _direction = Input.GetAxis(HORIZONTAL_AXIS);
        if (_isGround && Input.GetKeyDown(KeyCode.W))
        {
            _isJump = true;
        }
    }

    void FixedUpdate()
    {
        _rb.velocity = new Vector2(_speedX * _direction * SPEED_COEFFICIENT * Time.fixedDeltaTime, _rb.velocity.y);
        if (_isJump)
        {
            _rb.AddForce(new Vector2(0, _jumpForce));
            _isJump = false;
            _isGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            _isGround = true;
        }
    }
}
