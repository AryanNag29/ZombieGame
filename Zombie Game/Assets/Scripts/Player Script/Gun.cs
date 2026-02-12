using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    #region References

    [Header("References")] [SerializeField]
    private EnemyLogics _enemyLogics;

    #endregion
    
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

    #region Start

    private void Start()
    {
        _enemyLogics = GameObject.FindWithTag("Enemy").GetComponent<EnemyLogics>();
    }

    #endregion


    #region Functions

    public void Attack()
    {
        if (lastShotTime + shootingDelay < Time.time)
        {
            //not gonna use object pulling
            if (!shootingSystem.isPlaying)
            {
                shootingSystem.Play();
            }
            Vector3 direction = getDirection();

            if (Physics.Raycast(bulletSpawn.position, direction, out RaycastHit hit, float.MaxValue, _mask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawn.position, Quaternion.identity);

                StartCoroutine(spawnTrail(trail, hit));

                lastShotTime = Time.time;
            }
        }
    }

    public void StopAttacking()
    {
        if(shootingSystem.isPlaying) shootingSystem.Stop();
    }

    public void DealDamage()
    {
        if (lastShotTime + shootingDelay < Time.time)
        {
            //not gonna use object pulling
            if (!shootingSystem.isPlaying)
            {
                shootingSystem.Play();
            }
            Vector3 direction = getDirection();

            if (Physics.Raycast(bulletSpawn.position, direction, out RaycastHit hit, float.MaxValue, _mask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawn.position, Quaternion.identity);

                StartCoroutine(spawnTrail(trail, hit));

                if (hit.collider.CompareTag("Enemy"))
                {
                    if (_enemyLogics != null)
                    {
                        _enemyLogics.DealDamage();
                    }
                }
                
                
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
            yield return null;
        }

        trail.transform.position = hit.point;
        if (impactParticleSystem != null)
        {
            Instantiate(impactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
        }
        
        Destroy(trail.gameObject, trail.time);
    }

    #endregion
}