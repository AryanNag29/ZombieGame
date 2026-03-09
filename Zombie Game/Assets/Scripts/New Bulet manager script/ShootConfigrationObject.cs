using UnityEngine;

namespace ZombieGame
{
    [CreateAssetMenu(fileName = "Shoot Config", menuName = "Guns/Shoot Configration", order = 2)]
    public class ShootConfigrationObject : ScriptableObject
    {
        #region References

        public LayerMask HitMask;
        public Material material;

        #endregion


        #region Variables

        public Vector3 spread = new Vector3(0.1f, 0.1f, 0.1f);
        public float fireRate = 0.25f;

        #endregion
    }
}
