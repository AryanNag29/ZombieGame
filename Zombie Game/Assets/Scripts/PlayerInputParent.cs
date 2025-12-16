using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerInputParent : StateManager<PlayerInputParent.PlayerState>
{
    #region PlayerEnum
    public enum PlayerState
    {
        Idle,
        Walk,
        Run,
    }
    #endregion
    
    #region ComponentReference
    [SerializeField]protected CharacterController controls;
    #endregion

    #region Matrix
    protected Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));
    protected Vector3 multiplyMatrix(Vector3 _input) => isometricMatrix.MultiplyPoint3x4(_input);
    #endregion
    
    #region Variables
    //important Variables
    protected InputSystem_Actions _playerActions;
    
    [Header("Character Movement")]
    //movement variable
    protected Vector2 _Input;
    protected Vector3 _currentMovement;
    protected bool _isMovementPressed;
    
    [Header("Character Rotation")]
    protected Vector2 _inputRotation;
    protected Vector3 _currentRotation;
    
    [Header("Character Sprint")]
    protected bool _isSprintPressed;
    #endregion

    #region Functions
    
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
    }

    protected void OnSprint(InputAction.CallbackContext context)
    {
        _isSprintPressed = context.ReadValueAsButton();
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
        //to start the Rotation of character with controller
        _playerActions.Player.Look.performed += GatherInputOnRotation; 
        
        //sprint
        //keyboard input
        _playerActions.Player.Sprint.started += OnSprint;
        _playerActions.Player.Sprint.canceled += OnSprint;
        //controller input
        _playerActions.Player.Sprint.performed += OnSprint;
        _playerActions.Player.Sprint.performed -= OnSprint;
    }
    #endregion

    #region  OnEnable/Disable

    private void OnEnable()
    {
        //enable player character controles
        _playerActions.Player.Enable();
    }
    
    void OnDisable()
    {
        //disable player character contoles
        _playerActions.Player.Disable();
    }

    #endregion
}
