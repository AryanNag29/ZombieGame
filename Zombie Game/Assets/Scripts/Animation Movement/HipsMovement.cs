using UnityEngine;

public class HipsMovement : MonoBehaviour
{
    #region Variables

    private Quaternion hipsCurrentRotationLocal;
    private Quaternion hipsCurrentRotationWorld;
    private float hipsYRoration = 14.305f;

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        hipsCurrentRotationLocal = transform.rotation;
        Matrix4x4 mworld = transform.localToWorldMatrix;
        hipsCurrentRotationWorld = mworld.rotation;
        // Debug.Log(hipsCurrentRotationWorld.eulerAngles);
        if (Input.GetKeyDown(KeyCode.W))
        { 
            transform.rotation = Quaternion.Euler(hipsCurrentRotationWorld.x,hipsYRoration,hipsCurrentRotationWorld.z);
            Debug.Log(transform.rotation.eulerAngles);
        }
    }
}
