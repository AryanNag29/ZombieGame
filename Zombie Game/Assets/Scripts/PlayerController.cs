using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : PlayerInputParent
{
    #region Variables
    //variables
    protected float _currentSpeed;
    [SerializeField]protected float _maxSpeed = 5f;
    [SerializeField]protected float _rotationSmoothing = 3f;
    [SerializeField]protected float _accelerationFactor = 3f;
    [SerializeField]protected float _deaccelerationFactor = 30f;
    [SerializeField]protected float sprintingSpeed = 15f;
    [SerializeField]protected float sprintMultiplier = 3f;
    #endregion
    
    
    #region Functions
    //functions
    protected void ApplyMovement()
    {
        controls.Move(multiplyMatrix(_currentMovement) * _currentSpeed * Time.deltaTime);
    }

    protected void ApplyRotation()
    {
        Quaternion _SkewedRotaion = Quaternion.LookRotation(multiplyMatrix(_currentRotation), Vector3.up);//skewed rotation towards y axis
        transform.rotation = Quaternion.Slerp(transform.rotation , _SkewedRotaion, _rotationSmoothing * Time.deltaTime); // smoothing rotaion with slerp
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

    #region Awake

    void awake()
    {
    }

    #endregion
    
    #region Update
    private void Update()
    {
        CalculateSpeed();
        ApplyMovement();
        ApplyRotation();
        ApplySprint();
    }
    #endregion
}
