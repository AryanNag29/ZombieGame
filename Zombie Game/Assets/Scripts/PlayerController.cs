using System;
using UnityEngine;

public class PlayerController : PlayerInputParent
{
    //variables
    public float _movementSpeed = 5f;
    
    //functions
    protected void applyMovement()
    {
        _appliedMovement.x = _currentMovement.x;
        _appliedMovement.z = _currentMovement.z;
        controls.Move(_appliedMovement * _movementSpeed * Time.deltaTime);
    }

    protected void applyRotation()
    {
        var targetAngle = Mathf.Atan2(_inputRotation.x, _inputRotation.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
    }

    private void Update()
    {
        applyMovement();
        applyRotation();
    }
}
