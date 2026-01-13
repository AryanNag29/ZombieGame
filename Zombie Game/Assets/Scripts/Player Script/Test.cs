using UnityEngine;

public class Test : PlayerInputParent
{
        #region Variables
    //variables
    [Header("Movement")]
    protected float _currentSpeed;
    [SerializeField]protected float _maxSpeed = 5f;
    [SerializeField]protected float _accelerationFactor = 3f;
    [SerializeField]protected float _deaccelerationFactor = 30f;
    [Header("Sprint")]
    [SerializeField]protected float sprintingSpeed = 15f;
    [SerializeField]protected float sprintMultiplier = 3f;
    [Header("Rotation")]
    [SerializeField]protected float smoothing = 3f;
    [SerializeField] private float mouseSensitivity = 1.5f;
    protected bool receiveInput = true;
    protected float mouseX;
    protected float mouseY;
    private float rotationX = 0;
    private float rotationX_target = 0;
    private float rotationY_target = 0;
    private Vector3 rotation;
    protected Vector3 mouseInput;
    #endregion
    
    
    #region Functions
    //functions
    protected void GatherInput()
    {
        if (receiveInput)
        {
            multiplyMatrix(_currentRotation);
            mouseInput = _currentRotation;
        }
    }
    
    protected void ApplyMovement()
    {
        controls.Move(multiplyMatrix(_currentMovement) * _currentSpeed * Time.deltaTime);
        
    }

    protected void MouseRaycast()
    {
        
    }

    protected void ApplyRotation()
    {
      
    }
    
    protected void CalculateSpeed()
    {
            // if the input will become 0 form the keyboard and the current speed > 0 deceleration
            if (!_isMovementPressed && _currentSpeed > 0) 
            {
                _currentSpeed -= _deaccelerationFactor * Time.deltaTime;
            }
            //if the input is not zero and the current speed is less then max speed acceleration
            else if (_isMovementPressed && _currentSpeed < _maxSpeed)
            {
                _currentSpeed += _accelerationFactor * Time.deltaTime;
            }
            _currentSpeed = Mathf.Clamp(_currentSpeed,0,_maxSpeed);
    }

    protected void ApplySprint()
    {
        
        if (_isSprintPressed && _currentSpeed > 0 )
        {
            StartSprint();
        }
        else 
        {
            StopSprint();
        }

        _accelerationFactor = Mathf.Clamp(_accelerationFactor, 0, 15f);
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
    }

    protected override void StartSprint()
    {
        _maxSpeed = sprintingSpeed;
        _accelerationFactor *= sprintMultiplier;
        _currentSpeed += _accelerationFactor * Time.deltaTime;
    }

    protected override void StopSprint()
    {
        _maxSpeed = 5f; // Your walk speed
        _accelerationFactor = 3f;
    
        if (_currentSpeed > _maxSpeed)
        {
            _currentSpeed -= _deaccelerationFactor * Time.deltaTime;
        }
    }

    #endregion
    
    #region Start

    protected virtual void Start()
    {
    }

    #endregion
    
    #region Update
    protected virtual void Update()
    {
        CalculateSpeed();
        ApplyMovement();
        ApplyRotation();
        GatherInput();
        ApplySprint();
    }
    #endregion
}
