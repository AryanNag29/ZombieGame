using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Test : PlayerInputParent
{
    #region Variables
    //variables
    [Header("References")]
    [SerializeField] protected Camera _mainCamera;
    [SerializeField] protected LayerMask _groundLayer;
    
    [Header("Movement")]
    protected float _currentSpeed;
    [SerializeField]protected float _maxSpeed = 5f;
    [SerializeField]protected float _accelerationFactor = 3f;
    [SerializeField]protected float _deaccelerationFactor = 30f;
    [Header("Sprint")]
    [SerializeField]protected float sprintingSpeed = 15f;
    [SerializeField]protected float sprintMultiplier = 3f;
    [Header("Rotation")]
    [SerializeField]protected float gamepadSmoothing = 3f;
    [SerializeField]protected float mouseSmoothing = 3f;
    [SerializeField] private float mouseSensitivity = 1.5f;
    protected bool receiveInput = true;
    protected float mouseX;
    protected float mouseY;
    private float rotationX = 0;
    private float rotationX_target = 0;
    private float rotationY_target = 0;
    private Vector3 rotation;
    #endregion
    
    
    #region Functions
    //functions
    
    protected void ApplyMovement()
    {
        controls.Move(multiplyMatrix(_currentMovement) * _currentSpeed * Time.deltaTime);
        
    }

    protected void MouseRaycast()
    {
        Quaternion _SkewedRotaion = Quaternion.LookRotation(multiplyMatrix(_currentRotation), Vector3.up);//skewed rotation towards y axis
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 200f, _groundLayer))
        {
            Vector3 targetPostion = hit.point;
        
            Vector3 direction = targetPostion - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                Quaternion targetAngle = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * mouseSmoothing);
            }    
        }
        if (_isRotationPressed)
        {
            transform.rotation = Quaternion.LookRotation(_currentRotation, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation , _SkewedRotaion,  Time.deltaTime * gamepadSmoothing); // smoothing rotaion with slerp
        }
        
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
        MouseRaycast();
        ApplySprint();
    }
    #endregion
}
