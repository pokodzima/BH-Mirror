using Interfaces;
using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : NetworkBehaviour, IInputControllableAxis
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _rotationSpeed = 720f;
        private CharacterController _characterController;
        private Transform _cameraTransform;
        private float _horizontalInput;
        private float _verticalInput;
    
        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            GetComponent<NetworkTransform>().syncDirection = SyncDirection.ClientToServer;
        }
    
        void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }
            MoveCharacter();
            ApplyGravity();
        }

        private void ApplyGravity()
        {
            _characterController.Move(Physics.gravity * Time.deltaTime);
        }

        private void MoveCharacter()
        {
            Vector3 movementDirection = new Vector3(_horizontalInput, 0, _verticalInput);
            float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

            float speed = inputMagnitude * _speed;
            movementDirection = Quaternion.AngleAxis(_cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
            movementDirection.Normalize();

            _characterController.Move(movementDirection * (speed * Time.deltaTime));

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            }
        }


        public void SetCameraTransform(Transform cameraTransform)
        {
            _cameraTransform = cameraTransform;
        }

        public void GetHorizontalAxis(float axis)
        {
            _horizontalInput = axis;
        }

        public void GetVerticalAxis(float axis)
        {
            _verticalInput = axis;
        }
    }
}