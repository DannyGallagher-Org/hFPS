using System;
using hFPS.Bindings;
using InControl;
using UnityEngine;

namespace hFPS
{
    public class FpsLook : MonoBehaviour
    {
        // Inspector fields
        [SerializeField] private Transform playerBody;
        
        public float mouseSensitivity = 3f;
        private const float MouseSensitivityConstant = 100f;

        private PlayerActions _playerActions;

        private float _xRotation = 0f;

        private void Awake()
        {
            _playerActions = PlayerActions.CreateWithDefaultBindings();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            var lookX = _playerActions.Look.X * mouseSensitivity * MouseSensitivityConstant * Time.deltaTime;
            var lookY = _playerActions.Look.Y * mouseSensitivity * MouseSensitivityConstant * Time.deltaTime;

            _xRotation -= lookY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 90f);
            
            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * lookX);
        }
    }
}
