// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
// public class PlayerMover : MonoBehaviour
// {
//     private const string HORIZONTAL_AXIS = "Horizontal";
//     private const string VERTICAL_AXIS = "Vertical";

//     [SerializeField] private float _speed = 2;
//     [SerializeField] private float _dashForce = 5;

//     private Rigidbody2D _rigidbody;
//     private Vector3 _direction;
//     private float _directionX;
//     private float _directionY;
//     private bool _isDash = false;

//     private void Awake()
//     {
//         _rigidbody = GetComponent<Rigidbody2D>();
//     }

//     private void Update()
//     {
//         _directionX = Input.GetAxis(HORIZONTAL_AXIS);
//         _directionY = Input.GetAxis(VERTICAL_AXIS);

//         _direction = new Vector2(_directionX, _directionY).normalized;

//         // if (_direction != Vector3.zero)
//         // {
//         //     transform.up = -_direction.normalized;
//         // }
//         if (!_isDash && Input.GetKeyDown(KeyCode.Space))
//         {
//             _isDash = true;
//         }
//     }

//     private void FixedUpdate()
//     {
//         _rigidbody.velocity = _direction * _speed;
//         if (_isDash)
//         {
//             _rigidbody.AddForce(_direction * _dashForce, ForceMode2D.Impulse);
//             print(_direction);
//             print(_direction * _dashForce);
//             _isDash = false;
//         }
//     }
// }
