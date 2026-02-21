using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "Gun config" , menuName = "Guns/Gun" , order = 0)]
public class GunScriptObject : MonoBehaviour
{
    #region Variables/References
    
    //public members
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
    private ObjectPool<TrailRenderer>  trailPool;
    #endregion


    #region Functions


    public void Spawn(Transform Parent, MonoBehaviour ActiveMonobehaviour)
    {
        this.ActiveMonobehaviour = ActiveMonobehaviour;
        lastShootTime = 0f; // in editor this will not be properly rest, in built it's fine
        trailPool = new ObjectPool<TrailRenderer>(CreateTrail);
        model = Instantiate(modelPrefab);
        model.transform.SetParent(Parent,false);
        model.transform.localPosition = spawnPosition;
        model.transform.localRotation = Quaternion.Euler(spawnRotation);

        shootSystem = model.GetComponentInChildren<ParticleSystem>();
    }

    private IEnumerator playTrail(Vector3 startPoint, Vector3 endpoint, RaycastHit hit)
    {
        TrailRenderer instance = trailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = startPoint;
        
        yield return null;  //avoid position carry-over from last frame if reused
        
        instance.emitting = true;

        float distance = Vector3.Distance(startPoint, endpoint);
        float remainingDistance = distance;
        while (remainingDistance > 0f)
        {
            instance.transform.position = Vector3.Lerp(
                startPoint,
                endpoint,
                Mathf.Clamp01(1-(remainingDistance / distance))
                );
            remainingDistance -= trailConfig.simulationSpeed * Time.deltaTime;
            
            yield return null;
        }
        instance.transform.position = endpoint;
        
        yield return new WaitForSeconds(trailConfig.duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        trailPool.Release(instance);


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
        trail.shadowCastingMode = ShadowCastingMode.Off;
        return trail;
    }

    #endregion
    
    
}
