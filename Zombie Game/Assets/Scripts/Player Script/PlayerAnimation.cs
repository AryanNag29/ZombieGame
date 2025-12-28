using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerAnimation : PlayerController
{
    #region Variables

    private Animator _animator;
    

    #endregion

    #region Functions

    void isWalkingAnimation()
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
    

    #endregion

    #region Start

    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
    }

    #endregion
    
    #region Update

    protected override void Update()
    {
        isWalkingAnimation();
    }

    #endregion
}
