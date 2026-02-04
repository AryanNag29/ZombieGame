
    namespace LifelikeMotion.IKFootPlacement
    {
        using UnityEngine;

        public class BasicCharacterRotation : PlayerInputParent
        {
            #region Variables

            [SerializeField] private float mouseSensitivity = 1.5f;
            [SerializeField] private float mouseSmoothing = 0;
            [SerializeField] private float gamepadSmoothing = 0f;
            [SerializeField] private float smoothing = 0f;

            private Vector3 rotation;
            private Animator animator;
            private float mouseX;
            private float mouseY;
            private float rotationX = 0;
            private float rotationX_target = 0;
            private float rotationY_target = 0;
            private bool receiveInput = true;

            [SerializeField] private LayerMask _groundLayer;
            [SerializeField] private Camera _mainCamera;

            #endregion


            #region Start

            private void Start()
            {
                animator = GetComponent<Animator>();
                rotation.y = transform.eulerAngles.y;
            }

            #endregion

            #region Update

            private void Update()
            {
                GetInputData();
                ApplyRotation();
            }

            #endregion

            #region function

            private void ApplyRotation()
            {
                if (gamepadSmoothing <= 0 && mouseSmoothing <= 0 && _isRotationPressed)
                {
                    rotation.y += mouseX * mouseSensitivity;
                    rotationY_target = rotation.y;
                    rotationX_target += mouseX * mouseSensitivity;
                    rotationX_target = Mathf.Clamp(rotationX_target, -90, 90);
                    rotationX = rotationX_target;

                    float _rotation_Angle = rotationX_target / 90f;
                    animator.SetFloat("Rotation_Angle", _rotation_Angle);

                    Quaternion _SkewedRotaion =
                        Quaternion.LookRotation(multiplyMatrix(_currentRotation),
                            Vector3.up); //skewed rotation towards y axis
                    Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit, 200f, _groundLayer))
                    {
                        Vector3 targetPostion = hit.point;

                        Vector3 direction = targetPostion - transform.position;
                        direction.y = 0;
                        if (direction != Vector3.zero)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                            Quaternion targetAngle = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
                            transform.rotation =
                                Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * mouseSmoothing);
                        }
                    }

                    if (_isRotationPressed)
                    {
                        transform.rotation = Quaternion.LookRotation(_currentRotation, Vector3.up);
                        transform.rotation =
                            Quaternion.Slerp(transform.rotation, _SkewedRotaion,
                                Time.deltaTime * gamepadSmoothing); // smoothing rotaion with slerp
                    }
                }
                else if (mouseSmoothing > 0 || gamepadSmoothing > 0 && _isRotationPressed)
                {
                    rotationY_target += mouseX * mouseSensitivity;


                    rotationX_target += mouseX * mouseSensitivity;
                    rotationX_target = Mathf.Clamp(rotationX_target, -90, 90);

                    rotationX = Mathf.Lerp(rotationX, rotationX_target, Time.deltaTime / smoothing);
                    float _rotation_Angle = rotationX / 90f;
                    animator.SetFloat("Rotation_Angle", _rotation_Angle);

                    Quaternion _SkewedRotaion =
                        Quaternion.LookRotation(multiplyMatrix(_currentRotation),
                            Vector3.up); //skewed rotation towards y axis
                    Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit hit, 200f, _groundLayer))
                    {
                        rotation.y = Mathf.Lerp(rotation.y, rotationY_target, Time.deltaTime / mouseSmoothing);
                        Vector3 targetPostion = hit.point;
                        Vector3 direction = targetPostion - transform.position;
                        direction.y = 0;
                        if (direction != Vector3.zero)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                            Quaternion targetAngle = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
                            transform.rotation =
                                Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * mouseSmoothing);
                        }
                    }

                    if (_isRotationPressed)
                    {
                        rotation.y = Mathf.Lerp(rotation.y, rotationY_target, Time.deltaTime / gamepadSmoothing);
                        transform.rotation = Quaternion.LookRotation(_currentRotation, Vector3.up);
                        transform.rotation =
                            Quaternion.Slerp(transform.rotation, _SkewedRotaion,
                                Time.deltaTime * gamepadSmoothing); // smoothing rotaion with slerp
                    }
                }
            }

            private void GetInputData()
            {
                if (receiveInput)
                {
                    multiplyMatrix(_currentRotation);
                    mouseX = _currentRotation.x;
                    mouseY = _currentRotation.z;
                }
            }

            #endregion
        }
    }
    