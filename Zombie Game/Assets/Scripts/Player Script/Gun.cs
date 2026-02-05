using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
   #region Variables

   [SerializeField] private bool addBulletSpread = true;
   [SerializeField] private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
   []

   #endregion
}
