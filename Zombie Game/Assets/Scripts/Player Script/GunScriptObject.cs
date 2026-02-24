using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace ZombieGame
{
    [CreateAssetMenu(fileName = "Gun Config", menuName = "Guns/Gun configue", order = 0)]
    public class GunScriptObject : ScriptableObject
    {
        #region Variables/References

        //public members
        // public ImpactType impactType;
        public GunType gunType;
        public string name;
        public GameObject modelPrefab;
        public Vector3 spawnPosition;
        public Vector3 spawnRotation;

        public ShootConfigrationObject shootConfig;
        public TrailConfig trailConfig;

        //private members
        private MonoBehaviour ActiveMonobehaviour;
        private GameObject model;
        private float lastShootTime;
        private ParticleSystem shootSystem;
        private ObjectPool<TrailRenderer> trailPool;

        #endregion


        #region Functions

        public void Spawn(Transform Parent, MonoBehaviour ActiveMonobehaviour)
        {
            this.ActiveMonobehaviour = ActiveMonobehaviour;
            lastShootTime = 0f; // in editor this will not be properly rest, in built it's fine
            trailPool = new ObjectPool<TrailRenderer>(createFunc: CreateTrail);
            model = Instantiate(modelPrefab);
            model.transform.SetParent(Parent, false);
            model.transform.localPosition = spawnPosition;
            model.transform.localRotation = Quaternion.Euler(spawnRotation);

            shootSystem = model.GetComponentInChildren<ParticleSystem>();
        }

        private IEnumerator playTrail(Vector3 startPoint, Vector3 endpoint, RaycastHit hit)
        {
            TrailRenderer instance = trailPool.Get();
            instance.gameObject.SetActive(true);
            instance.transform.position = startPoint;

            yield return null; //avoid position carry-over from last frame if reused

            instance.emitting = true;

            float distance = Vector3.Distance(startPoint, endpoint);
            float remainingDistance = distance;
            while (remainingDistance > 0f)
            {
                instance.transform.position = Vector3.Lerp(
                    startPoint,
                    endpoint,
                    Mathf.Clamp01(1 - (remainingDistance / distance))
                );
                remainingDistance -= trailConfig.simulationSpeed * Time.deltaTime;

                yield return null;
            }

            instance.transform.position = endpoint;

            // if (hit.collider != null)
            //     SurfaceManager.Instance.HandleImpact(
            //     hit.transform.gameObject,
            //     EndPoint,
            //     Hit.normal,
            //     ImpactType,
            //     0
            // }

            yield return new WaitForSeconds(trailConfig.duration);
            yield return null;
            instance.emitting = false;
            instance.gameObject.SetActive(false);
            trailPool.Release(instance);
        }

        public void Shoot()
        {
            if (Time.time > shootConfig.fireRate + lastShootTime)
            {
                lastShootTime = Time.time;
                shootSystem.Play();
                Vector3 shootDirection = shootSystem.transform.forward + new Vector3(
                    Random.Range(-shootConfig.spread.x, shootConfig.spread.x),
                    Random.Range(-shootConfig.spread.y, shootConfig.spread.y),
                    Random.Range(-shootConfig.spread.z, shootConfig.spread.z)
                );
                shootDirection.Normalize();

                if (Physics.Raycast(shootSystem.transform.position,
                        shootDirection,
                        out RaycastHit hit,
                        float.MaxValue,
                        shootConfig.HitMask))

                {
                    ActiveMonobehaviour.StartCoroutine(
                        playTrail(
                            shootSystem.transform.position,
                            hit.point,
                            hit
                        )
                    );
                }
                else
                {
                    ActiveMonobehaviour.StartCoroutine(
                        playTrail(
                            shootSystem.transform.position,
                            shootSystem.transform.position + (shootDirection * trailConfig.missDistance),
                            new RaycastHit()
                        )
                    );
                }
            }
        }

        private TrailRenderer CreateTrail()
        {
            GameObject instance = new GameObject("Bullet Trail");
            TrailRenderer trail = instance.AddComponent<TrailRenderer>();
            trail.colorGradient = trailConfig.color;
            trail.material = trailConfig.material;
            trail.widthCurve = trailConfig.widthCurve;
            trail.time = trailConfig.duration;

            trail.emitting = false;
            trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            return trail;
        }

        #endregion
    }
}