using UnityEngine;

public class HipsMovement : MonoBehaviour
{
    #region Variables

    private Quaternion hipsCurrentRotation;
    private float hipsYRoration = 14.305f;

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     hipsCurrentRotation = transform.rotation;
        //     transform.rotation = new Quaternion(hipsCurrentRotation.x,hipsYRoration,hipsCurrentRotation.z,hipsCurrentRotation.w);
        // }
    }
}
