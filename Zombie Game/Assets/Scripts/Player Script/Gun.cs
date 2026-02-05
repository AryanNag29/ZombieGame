using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
   #region Variables

   [SerializeField] private bool addBulletSpread = true;
   [SerializeField] private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
   [SerializeField] private ParticleSystem shootingSystem;
   [SerializeField] private Transform bulletSpawn;
   [SerializeField] private ParticleSystem impactParticleSystem;
   [SerializeField] private float shootingDelay = 0.05f;
   [SerializeField] private LayerMask _mask;
   private Animator _animator;
   private float lastShotTime;

   #endregion

   #region Awake

   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   #endregion


   #region Functions

   public void Attack()
   {
      if (lastShotTime + shootingDelay < Time.time)
      {
         //not gonna use object pulling
      }
   }

   #endregion
}
