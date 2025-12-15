using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : PlayerInputParent
{
    #region Variables
    //variables
    protected float _currentSpeed;
    protected float _maxSpeed = 5f;
    protected float _rotationSmoothing = 3f;
    protected float _accelerationFactor = 3f;
    protected float _deaccelerationFactor = 30f;
    protected float sprintingSpeed = 15f;
    protected float sprintMultiplier = 3f;
    #endregion
    

    #region Functions
    //functions
    protected void applyMovement()
    {
        controls.Move(multiplyMatrix(_currentMovement) * _currentSpeed * Time.deltaTime);
    }

    protected void applyRotation()
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

    protected void applySprint()
    {
        if (_isSprintPressed && _currentSpeed > 0)
        {
            _maxSpeed = sprintingSpeed;
            _accelerationFactor *= sprintMultiplier;
            _currentSpeed += _accelerationFactor * Time.deltaTime;
        }
        else if (!_isSprintPressed && _maxSpeed == sprintingSpeed)
        {
            _maxSpeed = 5f;
            _accelerationFactor = 3f;
            _currentSpeed -= _deaccelerationFactor * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
    }

    #endregion



    #region Update
    private void Update()
    {
        CalculateSpeed();
        applyMovement();
        applyRotation();
        applySprint();
    }
    #endregion
}
