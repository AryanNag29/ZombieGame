using UnityEngine;

public class TwoDimentionAnimationStateControl : MonoBehaviour
{
    #region Variables

    Animator _animator;
    private float VelocityX = 0.0f;
    private float VelocityZ = 0.0f;
    [SerializeField] protected float velocityAccelration = 2.0f;

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool forwaredRightPressed = forwardPressed && Input.GetKey(KeyCode.D);
        bool forwaredLeftPressed = forwardPressed && Input.GetKey(KeyCode.A);
        bool backwardRightPressed = backwardPressed && Input.GetKey(KeyCode.D);
        bool backwardLeftPressed = backwardPressed && Input.GetKey(KeyCode.A);

        //clamp
        VelocityX = Mathf.Clamp(VelocityX, -0.5f, 0.5f);
        VelocityZ = Mathf.Clamp(VelocityZ, -0.5f, 0.5f);

        if (forwardPressed)
        {
            VelocityZ += Time.deltaTime * velocityAccelration;
        }

        if (backwardPressed)
        {
            VelocityZ -= Time.deltaTime * velocityAccelration;
        }

        if (leftPressed)
        {
            VelocityX -= Time.deltaTime * velocityAccelration;
        }

        if (rightPressed)
        {
            VelocityX += Time.deltaTime * velocityAccelration;
        }

        if (forwaredRightPressed)
        {
            VelocityZ += Time.deltaTime * velocityAccelration;
            VelocityX += Time.deltaTime * velocityAccelration;
        }

        if (forwaredLeftPressed)
        {
            VelocityZ += Time.deltaTime * velocityAccelration;
            VelocityX -= Time.deltaTime * velocityAccelration;
        }

        if (backwardRightPressed)
        {
            VelocityZ -= Time.deltaTime * velocityAccelration;
            VelocityX += Time.deltaTime * velocityAccelration;
        }

        if (backwardLeftPressed)
        {
            VelocityZ -= Time.deltaTime * velocityAccelration;
            VelocityX -= Time.deltaTime * velocityAccelration;
        }

        else if (!forwardPressed && !backwardPressed && !leftPressed && !rightPressed)
        {
            VelocityX = 0.0f;
            VelocityZ = 0.0f;
        }

        _animator.SetFloat("VelocityX", VelocityX);
        _animator.SetFloat("VelocityZ", VelocityZ);
    }
}