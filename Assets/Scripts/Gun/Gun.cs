using System;
using UnityEngine;

namespace Gun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float _launchForce = 1200;
        [SerializeField] private float _torqueForce;
        [SerializeField] private float _range = 10;
        [SerializeField, Range(0, 1)] private float _pullSpeed;
        [SerializeField] private GameObject _grabbedObject;
        [SerializeField] private Transform _pullTransform;
        [SerializeField] private Transform _camera;
        private void Update()
        {
            // Check if key pressed.
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Launch the object if one is already picked up.
                if (_grabbedObject)
                {
                    var grabbedRb = _grabbedObject.GetComponent<Rigidbody>();
                    grabbedRb.isKinematic = false;
                    grabbedRb.transform.parent = null;
                    grabbedRb.AddForce(_camera.forward * _launchForce);
                    grabbedRb.AddTorque(Vector3.forward * _torqueForce);
                    _grabbedObject = null;
                }
                // Otherwise try to grab it.
                else
                {
                    // Check what object is being looked at.
                    if (Physics.Raycast(_camera.position, _camera.forward, out var hitInfo, _range))
                    {
                        // Check if the object can be grabbed.
                        if (hitInfo.collider.CompareTag("Grabbable"))
                        {
                            _grabbedObject = hitInfo.collider.gameObject;
                            var grabbedRb = _grabbedObject.GetComponent<Rigidbody>();
                            grabbedRb.isKinematic = true;
                            var grabbedTransform = grabbedRb.transform;
                            grabbedTransform.parent = transform;
                        }
                    }
                }
            }
            // Pull the object smoothly if one is picked.
            if (_grabbedObject)
            {
                var grabbedTransform = _grabbedObject.transform;
                grabbedTransform.position = Vector3.Lerp(grabbedTransform.position, _pullTransform.position, _pullSpeed);
            }
        }
    }
}
