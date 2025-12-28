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
        bool isWalking = _animator.GetBool("IsWalking");
        if (!isWalking && _isMovementPressed)
        {
            _animator.SetBool(isWalkingHash, true);
        }
        if(isWalking && !_isMovementPressed)
        {
            _animator.SetBool(isWalkingHash,false);
        }
    }

    void IsRunningAnimation()
    {
        bool isRunning = _animator.GetBool("IsRunning");
        if (!isRunning && _isSprintPressed && _isMovementPressed)
        {
            _animator.SetBool(isRunningHash, true);
        }
        if (isRunning && !_isMovementPressed)
        {
            _animator.SetBool(isRunningHash,false);
        }
        else if(isRunning && !_isSprintPressed)
        {
            _animator.SetBool(isRunningHash,false);
        }
    }
    

    #endregion

    #region Start

    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
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
