using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector3 _input;

    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
    }
    
}
