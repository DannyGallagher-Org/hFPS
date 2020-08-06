using System;
using hFPS.Bindings;
using UnityEngine;

namespace hFPS
{
    public class FpsMove : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform groundCheck;
        
        [SerializeField] private float speed = 12f;
        [SerializeField] private float jumpHeight = 3f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;
        
        private PlayerActions _playerActions;
        private Transform _transform;

        private bool _isGrounded;
        private Vector3 _velocity;
        private float _momentum;

        private void Awake()
        {
            _transform = transform;
            _playerActions = PlayerActions.CreateWithDefaultBindings();
        }

        // Update is called once per frame
        void Update()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (_isGrounded && _velocity.y < 0f)
                _velocity.y = -2f;
            
            var moveX = _playerActions.Move.X;
            var moveZ = _playerActions.Move.Y;

            var move = _transform.right * moveX + _transform.forward * moveZ;

            if (move.magnitude > 0f && _momentum < 1f)
                _momentum += Time.deltaTime;
            else if (Math.Abs(move.magnitude) < 0.01f)
                _momentum = 0f;

            controller.Move(Time.deltaTime * speed * _momentum * move);

            if (_playerActions.Jump.WasPressed && _isGrounded)
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);
        }
    }
}
