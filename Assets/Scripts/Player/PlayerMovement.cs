using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private KeyCode _forwardKey = KeyCode.W;
        [SerializeField] private KeyCode _backwardsKey = KeyCode.S;
        [SerializeField] private KeyCode _leftKey = KeyCode.A;
        [SerializeField] private KeyCode _rightKey = KeyCode.D;
        [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
        [SerializeField] private float _speed, _jumpForce;
        [SerializeField] private int _maxJumps;
        [SerializeField] private Transform _cameraTransform;
        private int _jumpCount;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Work out the correct movement angles.
            var forward = _cameraTransform.forward;
            var right = _cameraTransform.right;
            var cameraForward = new Vector3(forward.x, 0, forward.z);
            var cameraRight = new Vector3(right.x, 0, right.z);
            // Get movement input.
            var moveForce = Vector3.zero;
            if (Input.GetKey(_forwardKey))
            {
                moveForce += cameraForward;
            }
            else if (Input.GetKey(_backwardsKey))
            {
                moveForce -= cameraForward;
            }

            if (Input.GetKey(_rightKey))
            {
                moveForce += cameraRight;
            }
            else if (Input.GetKey(_leftKey))
            {
                moveForce -= cameraRight;
            }
            // Jumping.
            if (Input.GetKeyDown(_jumpKey) && _jumpCount < _maxJumps)
            {
                _jumpCount++;
                // Reset the Y velocity.
                var velocity = _rb.velocity;
                velocity = new Vector3(velocity.x, 0, velocity.z);
                _rb.velocity = velocity;
                _rb.AddForce(Vector3.up * _jumpForce);
            }
            // Move the player.
            _rb.MovePosition(transform.position + moveForce * _speed);
        }

        private void OnCollisionEnter(Collision other)
        {
            _jumpCount = 0;
        }
    }
}
