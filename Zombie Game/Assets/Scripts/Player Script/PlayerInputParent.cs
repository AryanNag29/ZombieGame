
using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

    public abstract class PlayerInputParent : MonoBehaviour
    {
        #region ComponentReference

        [SerializeField] protected CharacterController controls;

        #endregion

        #region Matrix

        protected Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));
        protected Vector3 multiplyMatrix(Vector3 _input) => isometricMatrix.MultiplyPoint3x4(_input);

        #endregion

        #region Variables

        //important Variables
        protected InputSystem_Actions _playerActions;

        [Header("Animator")] [SerializeField] protected float animationVelocity = 0.0f;
        protected float animationAcceleration = 0.1f;
        protected float animationDeceleration = 0.5f;
        protected int velocityHash;

        [Header("Character Movement")]
        //movement variable
        protected Vector2 _Input;

        protected Vector3 _currentMovement;
        [SerializeField] protected bool _isMovementPressed;

        [Header("Character Rotation")] protected Vector2 _inputRotation;
        protected Vector3 _currentRotation;
        [SerializeField] protected bool _isRotationPressed;

        [Header("Character Sprint")] [SerializeField]
        protected bool _isSprintPressed = false;

        [Header("Shoot")] [SerializeField] public bool _shoot;

        #endregion

        #region Functions

        protected virtual void StartSprint()
        {
        }

        protected virtual void StopSprint()
        {
        }

        protected void GatherInputOnMovement(InputAction.CallbackContext context) //for movement function
        {
            //movement of character
            _Input = context.ReadValue<Vector2>();
            _currentMovement.x = _Input.x;
            _currentMovement.z = _Input.y;
            _isMovementPressed = _Input.x != 0 || _Input.y != 0;
        }

        protected void GatherInputOnRotation(InputAction.CallbackContext context)
        {
            //rotation of Character
            _inputRotation = context.ReadValue<Vector2>();
            _currentRotation.x = _inputRotation.x;
            _currentRotation.z = _inputRotation.y;
            _isRotationPressed = _inputRotation.sqrMagnitude > 0.1f;
        }

        protected void OnAttack(InputAction.CallbackContext context)
        {
            _shoot = true;
            if (context.canceled)
            {
                _shoot = false;
            }
        }

        protected void OnSprint(InputAction.CallbackContext context)
        {
            _isSprintPressed = !_isSprintPressed;
            if (_isSprintPressed)
            {
                StartSprint();
            }
            else
            {
                StopSprint();
            }
        }

        #endregion

        #region Awake

        private void Awake()
        {
            controls = GetComponent<CharacterController>();
            _playerActions = new InputSystem_Actions();
            //Movement
            //to start the movement of character with keyboard
            _playerActions.Player.Move.started += GatherInputOnMovement;
            //to stop the movement of character with keyboard
            _playerActions.Player.Move.canceled += GatherInputOnMovement;
            //to start the movement of character with controller
            _playerActions.Player.Move.performed += GatherInputOnMovement;

            //Rotation
            //to start the Rotation of character with Mouse
            _playerActions.Player.Look.started += GatherInputOnRotation;
            //to stop the rotatin of character with Mouse
            _playerActions.Player.Look.canceled += GatherInputOnRotation;
            //to start the Rotation of character with controller
            _playerActions.Player.Look.performed += GatherInputOnRotation;

            //sprint
            //keyboard input
            _playerActions.Player.Sprint.started += OnSprint;
            _playerActions.Player.Sprint.canceled += OnSprint;

            //Attack(Shoot)
            _playerActions.Player.Attack.started += OnAttack;
            _playerActions.Player.Attack.canceled += OnAttack;
            _playerActions.Player.Attack.performed += OnAttack;
        }

        #endregion


        #region OnEnable/Disable

        private void OnEnable()
        {
            //enable player character controles
            _playerActions.Player.Enable();
            //controller input
            _playerActions.Player.Sprint.performed += OnSprint;
        }

        void OnDisable()
        {
            //disable player character contoles
            _playerActions.Player.Disable();
            _playerActions.Player.Sprint.performed -= OnSprint;
        }

        #endregion
    }
