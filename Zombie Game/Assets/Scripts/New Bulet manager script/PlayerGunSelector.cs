using System.Collections.Generic;
using UnityEngine;

namespace ZombieGame
{
    public class PlayerGunSelector : MonoBehaviour
    {

        #region References
        [SerializeField] private GunType Gun;
        [SerializeField] private Transform GunParent;
        [SerializeField] private List<GunScriptObject> Guns;
        #endregion

        #region Variables
        [Space] [Header("Runtime Filled")] public GunScriptObject ActiveGun;
        
        #endregion
    }
}