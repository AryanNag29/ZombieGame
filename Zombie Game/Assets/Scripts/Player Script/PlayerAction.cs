using System;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    #region References
    [SerializeField] protected Gun gun;
    [SerializeField] protected PlayerInputParent playerInput;
    [SerializeField] protected InputSystem_Actions controls;
    #endregion

    #region Variables

    protected bool onattack;

    #endregion

    #region Funtions

    private void OnAttack()
    {
        onattack = true;
        gun.attack();
    }

    #endregion

    #region Awake

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    #endregion

    #region Enable/Disable

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    #endregion
}
