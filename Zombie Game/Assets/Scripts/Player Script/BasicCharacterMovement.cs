namespace LifelikeMotion.IKFootPlacement
{
    using UnityEngine;

    public class BasicCharacterMovement : PlayerInputParent
    {
        private CharacterController cc;
        [SerializeField] private IKFootPlacement iKFootPlacement;
        [SerializeField] private float movementSpeed = 2;
        [SerializeField] private float jumpSpeed = 5;
        [SerializeField] private float gravity = 15;

        [Header("Movement")] protected float _currentSpeed;
        [SerializeField] protected float _maxSpeed = 5f;
        [SerializeField] protected float _accelerationFactor = 3f;
        [SerializeField] protected float _deaccelerationFactor = 30f;

        [Header("Sprint")] [SerializeField] protected float sprintingSpeed = 15f;
        [SerializeField] protected float sprintMultiplier = 3f;

        private bool receiveInput = true;
        private bool isMoving = true;
        private float horizontal;
        private float vertical;
        [HideInInspector] public bool jumped;

        private Vector3 velocity;
        private Vector3 ccPosition;
        private Animator animator;

        void Start()
        {
            cc = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();

            //Optional cursor lock and disabled visability
            // Cursor.visible = false;
            // Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            GetInputData();
            CalculateMovement();
            ApplySprint();
        }

        public void CalculateMovement()
        {
            Vector3 _velocity = Vector3.zero;
            _velocity.z = vertical;
            _velocity.x = horizontal;

            animator.SetFloat("Z", vertical);
            animator.SetFloat("X", horizontal);

            _velocity = Vector3.ClampMagnitude(_velocity, 1);

            velocity.z = _velocity.z * _currentSpeed;
            velocity.x = _velocity.x * _currentSpeed;


            if (cc.isGrounded && !jumped)
            {
                velocity.y = -2;
            }

            else if (cc.isGrounded && jumped)
            {
                velocity.y = jumpSpeed;
                if (iKFootPlacement != null)
                {
                    iKFootPlacement.isGrounded = false;
                    iKFootPlacement.jumped = true;
                }

                isMoving = true;
                jumped = false;
            }

            else
            {
                velocity.y -= gravity * Time.deltaTime;
                isMoving = true;
            }

            cc.Move(multiplyMatrix(velocity) * Time.deltaTime);

            if (!isMoving)
            {
                cc.transform.position = new Vector3(ccPosition.x, cc.transform.position.y, ccPosition.z);
            }
            else
            {
                ccPosition = cc.transform.position;
            }
        }

        protected void ApplySprint()
        {
            if (_isSprintPressed && _currentSpeed > 0)
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

        private void GetInputData()
        {
            if (receiveInput)
            {
                vertical = _currentMovement.z;
                horizontal = _currentMovement.x;

                if (iKFootPlacement != null)
                {
                    iKFootPlacement.isGrounded = cc.isGrounded;
                }

                if (vertical != 0 || horizontal != 0)
                {
                    isMoving = true;
                    if (iKFootPlacement != null) iKFootPlacement.isMoving = true;
                }
                else
                {
                    isMoving = false;
                    if (iKFootPlacement != null) iKFootPlacement.isMoving = false;
                }

                if (Input.GetAxis("Jump") > 0 && !jumped)
                {
                    jumped = true;
                }
                else
                {
                    jumped = false;
                }
            }
        }
    }
}