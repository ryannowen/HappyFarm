using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private KeyCode _forward;
        [SerializeField] private KeyCode _backwards;
        [SerializeField] private KeyCode _left;
        [SerializeField] private KeyCode _right;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _cameraTransform;
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
            // Get input.
            var moveForce = Vector3.zero;
            if (Input.GetKey(_forward))
            {
                moveForce += cameraForward;
            }
            else if (Input.GetKey(_backwards))
            {
                moveForce -= cameraForward;
            }

            if (Input.GetKey(_right))
            {
                moveForce += cameraRight;
            }
            else if (Input.GetKey(_left))
            {
                moveForce -= cameraRight;
            }
            // Move the player.
            _rb.MovePosition(transform.position + moveForce * _speed);
        }
    }
}
