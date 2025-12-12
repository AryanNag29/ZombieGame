using System;
using UnityEngine;

public class PlayerController : PlayerInputParent
{
    #region Variables
    //variables
    private float _currentSpeed;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 360f;
    [SerializeField] private float _rotationSmoothing = 5f;
    [SerializeField] private float _accelerationFactor = 5f;
    [SerializeField] private float _deaccelerationFactor = 30f;
    [SerializeField] private float sprintMultiplier = 2f;
    #endregion

    #region Functions
    //functions
    protected void applyMovement()
    {
        _appliedMovement.x = _currentMovement.x;
        _appliedMovement.z = _currentMovement.z;
        controls.Move(_appliedMovement * _currentSpeed * Time.deltaTime);
    }

    protected void applyRotation()
    {
        float targetAngle = Mathf.Atan2(_inputRotation.x, _inputRotation.y) * Mathf.Rad2Deg; //angle of rotation in degree
        Quaternion target  = Quaternion.Euler(0, targetAngle, 0);// rotation along y axis
        transform.rotation = Quaternion.Slerp(transform.rotation, target, _rotationSmoothing * Time.deltaTime); // smoothing rotaion with slerp
    }
    #endregion

    protected void CalculateSpeed()
    {
        // if the input will become 0 form the keyboard and the current speed is greater then 0 deceleration
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

    #region Update
    private void Update()
    {
        CalculateSpeed();
        applyMovement();
        applyRotation();
    }
    #endregion
}
