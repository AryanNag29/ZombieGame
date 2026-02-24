using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

namespace ZombieGame
{
    public class PlayerAnimation : PlayerController
    {
        #region Variables

        private Animator _animator;

        #endregion

        #region Functions

        void IsWalkingAnimation()
        {
            // _animator.SetFloat(velocityHash, animationVelocity);
            // if (_isMovementPressed && animationVelocity >= 0)
            // {
            //     animationVelocity += Time.deltaTime * animationAcceleration;
            // }
            // else if(!_isMovementPressed && animationVelocity > 0)
            // {
            //     animationVelocity -= Time.deltaTime * animationDeceleration;
            // }
            // else if (animationVelocity < 0)
            // {
            //     animationVelocity = 0.0f;
            // }
        }

        void IsRunningAnimation()
        {
            // _animator.SetFloat(velocityHash, animationVelocity);
            // if (_isMovementPressed && _isSprintPressed && animationVelocity >= 0)
            // {
            //     animationVelocity += Time.deltaTime * animationAcceleration;
            // }
            // else if (!_isMovementPressed || !_isSprintPressed)
            // {
            //     animationVelocity -= Time.deltaTime * animationDeceleration;
            // }
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
}