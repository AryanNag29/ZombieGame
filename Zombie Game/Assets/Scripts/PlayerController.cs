using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region ComponentReference
    [SerializeField] CharacterController controls;
    private PlayerInput playerinput;
    #endregion

    #region Variables
    //variables
    [Header("Gather Input")]
    private InputSystem_Actions _playerActions;
    
    [Header("Character Movement")]
    //movement variable
    private Vector2 _Input;
    private Vector3 _currentMovement;
    private Vector3 _appliedMovement;
    private bool _isMovementPressed;
    private float _movementSpeed = 5;
    [Header("Character Rotation")]
    private Vector2 _inputRotation;
    Vector3 _currentRotation;

    #endregion

    #region Functions

    //functions
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
    private void applyMovement()
    {
        _appliedMovement.x = _currentMovement.x;
        _appliedMovement.z = _currentMovement.z;
        controls.Move(_appliedMovement * _movementSpeed * Time.deltaTime);
    }

    private void applyRotation()
    {
        var targetAngle = Mathf.Atan2(_inputRotation.x, _inputRotation.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
    }

    #endregion

    

    #region Awake 

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
        
        //to start the movement of character with keyboard
        _playerActions.Player.Look.started += GatherInputOnRotation;
        //to start the movement of character with controller
        _playerActions.Player.Look.performed += GatherInputOnRotation; 
    }

    #endregion


    #region Update

    // Update is called once per frame
    void Update()
    {
        applyRotation();
        applyMovement();
    }

    #endregion


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
