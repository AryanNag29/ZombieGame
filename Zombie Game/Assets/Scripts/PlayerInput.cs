using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region MainVariables
    
    private InputSystem_Actions _playerActions;
    [SerializeField] private float playerSpeed = 1f;
  
    private Vector3 _input;

    #endregion

    #region OnAwake

    private void Awake()
    {
        _playerActions = new InputSystem_Actions();
    }

    #endregion

    #region FixedUpdate

    private void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region OnEnable/OnDisable

    private void OnEnable()
    {
        _playerActions.Player.Enable(); // This calls the input Action and enable it
    }

    private void OnDisable()
    {
        _playerActions.Player.Disable(); //This calls the player input action and disable it
    }

    #endregion

    #region Update

    private void Update()
    {
        GatherInput();
    }

    #endregion


    #region Functions
    
    void GatherInput()
    {
        Vector2 input = _playerActions.Player.Move.ReadValue<Vector2>();
        _input = new Vector3(input.x, 0f , input.y);
        Debug.Log(_input);
    }

    void Move()
    {
        
    }

    #endregion


}
