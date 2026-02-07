using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    #region Variables

    [SerializeField] private bool addBulletSpread = true;
    [SerializeField] private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private ParticleSystem shootingSystem;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private ParticleSystem impactParticleSystem;
    [SerializeField] private float shootingDelay = 0.05f;
    [SerializeField] private TrailRenderer bulletTrail;
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
            shootingSystem.Play();
            Vector3 direction = getDirection();

            if (Physics.Raycast(bulletSpawn.position, direction, out RaycastHit hit, float.MaxValue, _mask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawn.position, Quaternion.identity);

                StartCoroutine(spawnTrail(trail, hit));

                lastShotTime = Time.time;
            }
        }
    }

    private Vector3 getDirection()
    {
        Vector3 direction = transform.forward;
        if (addBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x),
                Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y),
                Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)
            );
            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator spawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0f;
        Vector3 startPostion = trail.transform.position;
    
        while (time < 1f)
        {
            trail.transform.position = Vector3.Lerp(startPostion, hit.point, time);
            time += Time.deltaTime / trail.time;
        }
        trail.transform.position = hit.point;
        Instantiate(GetComponent<ParticleSystem>(), hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(trail,trail.time);
        yield return null;
    }

#endregion
}