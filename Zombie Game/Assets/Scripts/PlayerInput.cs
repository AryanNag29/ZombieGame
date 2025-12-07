using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region MainVariables

    private Vector3 _input;

    #endregion

    #region Update

    private void Update()
    {
        GatherInput();
        Move();
    }

    #endregion


    #region Functions
    
    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
    }

    void Move()
    {
        
    }

    #endregion


}
