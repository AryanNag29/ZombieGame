using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerAnimation : PlayerController
{
    #region Variables
    private Animator _animator;
    #endregion

    #region Functions

    void IsWalkingAnimation()
    {
        _animator.SetFloat(velocityHash, animationVelocity);
        if (_isMovementPressed && animationVelocity >= 0)
        {
            animationVelocity += Time.deltaTime * animationAcceleration;
            animationVelocity = Mathf.Clamp(animationVelocity, 0.0f, 0.3f);
        }
        else
        {
            animationVelocity -= Time.deltaTime * animationDeceleration;
            animationVelocity = Mathf.Clamp(animationVelocity, 0.0f, 1f);
        }
    }

    void IsRunningAnimation()
    {
        _animator.SetFloat(velocityHash, animationVelocity);
        if (_isMovementPressed && _isSprintPressed && animationVelocity >= 0)
        {
            animationVelocity += Time.deltaTime * animationAcceleration;
            animationVelocity = Mathf.Clamp(animationVelocity, 0.0f, 1f);
        }
        else if (!_isMovementPressed || !_isSprintPressed)
        {
            animationVelocity -= Time.deltaTime * animationDeceleration;
            animationVelocity = Mathf.Clamp(animationVelocity, 0.0f, 1f);
        }
    }
    

    #endregion

    #region Start

    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");
    }

    #endregion
    
    #region Update

    protected override void Update()
    {
        IsWalkingAnimation();
        IsRunningAnimation();
    }

    #endregion
}
