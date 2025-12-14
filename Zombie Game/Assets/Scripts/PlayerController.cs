using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : PlayerInputParent
{
    #region Variables
    //variables
    private float _currentSpeed;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _rotationSmoothing = 3f;
    [SerializeField] private float _accelerationFactor = 5f;
    [SerializeField] private float _deaccelerationFactor = 30f;
    [SerializeField] private float sprintMultiplier = 2f;
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

    #endregion



    #region Update
    private void Update()
    {
        CalculateSpeed();
        applyMovement();
        applyRotation();
    }
    #endregion
}
