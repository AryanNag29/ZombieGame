using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Config", menuName = "Guns/Shoot Configration" , order = 2)]
public class ShootConfigrationObject : ScriptableObject
{
    #region References

    private LayerMask HitMask;
    public Material material;
    
    #endregion


    #region Variables

    private Vector3 spread = new Vector3(0.1f, 0.1f, 0.1f);
    private float fireRate = 0.25f;

    #endregion
}
