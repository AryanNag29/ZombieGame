using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    #region References
    [SerializeField] protected Gun gun;
    [SerializeField] protected PlayerInputParent playerInput;
    #endregion

    #region Variables

    protected bool onattack;

    #endregion

    #region Funtions

    private void OnShoot()
    {
        onattack = playerInput._shoot;
    }

    #endregion
}
