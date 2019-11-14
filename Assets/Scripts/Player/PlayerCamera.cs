using System;
using UnityEngine;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Vector2 _sensitivity = Vector2.one;
        [SerializeField] private Vector2 _pitchBounds;
        private Vector3 _targetAngle;

        private void Awake()
        {
            transform.eulerAngles = _targetAngle;
        }

        private void Update()
        {
            // Get input.
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
            _targetAngle.y += mouseX * _sensitivity.x;
            _targetAngle.x -= mouseY * _sensitivity.y;
            _targetAngle.x = Mathf.Clamp(_targetAngle.x, _pitchBounds.x, _pitchBounds.y);
            // Change the angle of the camera.
            transform.eulerAngles = _targetAngle;
        }
    }
}
