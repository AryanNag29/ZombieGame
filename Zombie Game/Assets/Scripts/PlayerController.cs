using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
        #region ComponentReference
    [SerializeField] CharacterController controls;
    private PlayerInput playerinput;
    #endregion

    //variables
    [Header("Character Movement")]
    //movement variable
    private Vector2 _Input;
    private InputSystem_Actions _playerActions;
    private Vector3 _currentMovement;
    private Vector3 _appliedMovement;
    private bool _isMovementPressed;
    private float _movementSpeed = 5;
    [Header("Gravity and Jump")]
    //gravity variable
    private float _gravity = -9.8f;
    private float _groundedVelocity = -0.05f;
    [SerializeField] private float fallMultiplier = 2f;
    private float _velocity;
    //jump variable
    public  bool isJumpPressed = false;
    private float _jumpVelocity = 20f;
    private float _initialJumpVelocity;
    private float _maxJumpHeight = 2f;
    private float _maxJumpTIme = 0.75f;
    private bool _isjumping = false;


    #region OnMovement

    //functions
    public void onMovement(InputAction.CallbackContext context) //for movement function
    {
        //movement of character
        _Input = context.ReadValue<Vector2>();
        _currentMovement.x = _Input.x;
        _currentMovement.z = _Input.y;
        _isMovementPressed = _Input.x != 0 || _Input.y != 0;
    }
    private void applyMovement()
    {
        _appliedMovement.x = _currentMovement.x;
        _appliedMovement.z = _currentMovement.z;
        controls.Move(_appliedMovement * _movementSpeed * Time.deltaTime);
    }

    #endregion


    #region OnJump
    private void setupJumpVariable()
    {
        float timeToApex = _maxJumpTIme / 2;
        _gravity = (-2 * _maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / timeToApex;
    }

    private void handleJumping()
    {
        if (!_isjumping && controls.isGrounded && isJumpPressed)
        {
            _isjumping = true;
            _currentMovement.y = _initialJumpVelocity;
        }
        else if (!isJumpPressed && _isjumping && controls.isGrounded)
        {
            _isjumping = false;
        }

    }

    public void onJump(InputAction.CallbackContext context)
    {
        //jump movement read
        isJumpPressed = context.ReadValueAsButton();
    }

    #endregion

    #region Gravity

    void applyGravity() // for gravity function
    {
        bool isFalling = _currentMovement.y <= 0.0f || !isJumpPressed;
        if (controls.isGrounded)
        {
            _currentMovement.y += _groundedVelocity;
        }
        else if (isFalling)
        {
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y = _currentMovement.y + (_gravity * fallMultiplier * Time.deltaTime);
            _appliedMovement.y = Mathf.Max((previousYVelocity + _currentMovement.y) * 0.5f,-20.0f);
        }
        else
        {
            //velocity varlet integration rule for gravity
            float previousYVelocity = _currentMovement.y;
            _currentMovement.y = _currentMovement.y + (_gravity * Time.deltaTime);// mathf.pow for the gravity because gravity always change so it need to multiply with delta time twice like 9.81 m/s^2
            _appliedMovement.y = (previousYVelocity + _currentMovement.y) * 0.5f; 
        }
    }

    #endregion


    #region Awake 

    void Awake()
    {
        controls = GetComponent<CharacterController>();
        playerinput = new PlayerInput();
        _playerActions = new InputSystem_Actions();
        //to start the movement of character with keyboard
        _playerActions.Player.Move.started += onMovement;
        //to stop the movement of character with keyboard
        _playerActions.Player.Move.canceled += onMovement;
        //to start the movement of character with controller
        _playerActions.Player.Move.performed += onMovement; 

        //player jump read 
        _playerActions.Player.Jump.started += onJump;
        _playerActions.Player.Jump.canceled += onJump;
        setupJumpVariable();
    }

    #endregion


    #region Update

    // Update is called once per frame
    void Update()
    {
        var targetAngle = Mathf.Atan2(_Input.x, _Input.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        applyGravity();
        handleJumping();
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
