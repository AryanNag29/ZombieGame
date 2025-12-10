using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputParent : MonoBehaviour
{
    #region ComponentReference
    [SerializeField]protected CharacterController controls;
    protected PlayerInput playerinput;
    #endregion
    
    private InputSystem_Actions _playerActions;
    
    [Header("Character Movement")]
    //movement variable
    protected Vector2 _Input;
    protected Vector3 _currentMovement;
    protected Vector3 _appliedMovement;
    protected bool _isMovementPressed;
    
    [Header("Character Rotation")]
    protected Vector2 _inputRotation;
    Vector3 _currentRotation;
    
    
    public void GatherInputOnMovement(InputAction.CallbackContext context) //for movement function
    {
        //movement of character
        _Input = context.ReadValue<Vector2>();
        _currentMovement.x = _Input.x;
        _currentMovement.z = _Input.y;
        _isMovementPressed = _Input.x != 0 || _Input.y != 0;
        
    }
    public void GatherInputOnRotation(InputAction.CallbackContext context)
    {
        //rotation of Character
        _inputRotation = context.ReadValue<Vector2>();
    }
    
    
    
    
    void Awake()
    {
        controls = GetComponent<CharacterController>();
        playerinput = new PlayerInput();
        _playerActions = new InputSystem_Actions();
        //to start the movement of character with keyboard
        _playerActions.Player.Move.started += GatherInputOnMovement;
        //to stop the movement of character with keyboard
        _playerActions.Player.Move.canceled += GatherInputOnMovement;
        //to start the movement of character with controller
        _playerActions.Player.Move.performed += GatherInputOnMovement; 
        
        //to start the Rotation of character with Mouse
        _playerActions.Player.Look.started += GatherInputOnRotation;
        //to start the Rotation of character with controller
        _playerActions.Player.Look.performed += GatherInputOnRotation; 
    }
    
    
    #region  OnEnable

    void OnEnable()
    {
        //enable player character controles
        _playerActions.Player.Enable();
    }

    #endregion


    #region OnDisable

    void OnDisable()
    {
        //disable player character contoles
        _playerActions.Player.Disable();
    }

    #endregion
}
