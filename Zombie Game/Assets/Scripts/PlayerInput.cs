using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    #region MainVariables
    
    private InputSystem_Actions _playerActions;
  
    private Vector3 _input;

    private CharacterController _characterController;

    #endregion

    #region Variables

    [SerializeField] private float MaxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float accelerationFactor = 5f;
    [SerializeField] private float decelerationFactor = 10f;
    private float _currentSpeed;
    
    #endregion

    #region OnAwake

    private void Awake()
    {
        _playerActions = new InputSystem_Actions();
        _characterController =  GetComponent<CharacterController>();
    }

    #endregion
    
    #region OnEnable/OnDisable

    private void OnEnable()
    {
        _playerActions.Player.Enable(); // This calls the input Action and enable it
    }

    private void OnDisable()
    {
        _playerActions.Player.Disable(); //This calls the player input action and disable it
    }

    #endregion

    #region Update

    private void Update()
    {
        GatherInput();
        Move();
        Look();
        CalculateSpeed();
    }

    #endregion


    #region Functions
    
    void GatherInput()
    {
        Vector2 input = _playerActions.Player.Move.ReadValue<Vector2>(); // read the value from the _input
        _input = new Vector3(input.x, 0f , input.y); //store the value in a vector 3 compo in the x and z
    }

    void Look()
    {
        if(_input == Vector3.zero) return; //if the player is not providing anything don't do anything 

        Matrix4x4 isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));
        Vector3 multiplyMatrix = isometricMatrix.MultiplyPoint3x4(_input);
        Quaternion rotation = Quaternion.LookRotation(multiplyMatrix, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation ,rotationSpeed * Time.deltaTime);
    }

    void Move()
    {
        Vector3 moveDirection = transform.forward *_input.magnitude  * _currentSpeed *  Time.deltaTime;
        _characterController.Move(moveDirection);
    }

    void CalculateSpeed()
    {
        if (_input == Vector3.zero && _currentSpeed > 0)
        {
            _currentSpeed -= decelerationFactor * Time.deltaTime;
        }
        else if (_input != Vector3.zero && _currentSpeed < MaxSpeed)
        {
            _currentSpeed += accelerationFactor * Time.deltaTime;
        }
        _currentSpeed = Mathf.Clamp(_currentSpeed,0,MaxSpeed);
    }

    #endregion


}
