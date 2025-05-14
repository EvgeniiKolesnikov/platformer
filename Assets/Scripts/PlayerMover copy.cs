// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]

// public class PlayerMover : MonoBehaviour
// {
//     private const float SPEED_COEFFICIENT = 50;
//     private const string HORIZONTAL_AXIS = "Horizontal";
//     private const string GROUND_TAG = "Ground";

//     [SerializeField] private float _speedX = 1;
//     [SerializeField] private float _jumpForce = 500;

//     private Rigidbody2D _rigidbody;
//     private float _direction;
//     private bool _isJump;
//     private bool _isGround;

//     private void Start()
//     {
//         _rigidbody = GetComponent<Rigidbody2D>();
//     }

//     private void Update()
//     {
//         _direction = Input.GetAxis(HORIZONTAL_AXIS);
//         if (_isGround && Input.GetKeyDown(KeyCode.W))
//         {
//             _isJump = true;
//         }
//     }

//     private void FixedUpdate()
//     {
//         _rigidbody.velocity = new Vector2(
//             _speedX
//           * _direction
//           * SPEED_COEFFICIENT
//           * Time.fixedDeltaTime,
//             _rigidbody.velocity.y);
//         if (_isJump)
//         {
//             _rigidbody.AddForce(new Vector2(0, _jumpForce));
//             _isJump = false;
//             _isGround = false;
//         }
//     }

//     void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag(GROUND_TAG))
//         {
//             _isGround = true;
//         }
//     }
// }
