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
        
        

    }

    #endregion
    
    
}
