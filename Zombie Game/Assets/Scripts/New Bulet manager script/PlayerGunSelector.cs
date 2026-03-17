using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieGame
{
    [DisallowMultipleComponent]
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


        #region Start

        private void Start()
        {
            GunScriptObject gun = Guns.Find(gun => gun.gunType == Gun);
            if(gun == null)
            {
                Debug.LogError($"No GunScriptableObject Found For GunType: {gun}");
                return;
            }

            ActiveGun = gun;
            gun.Spawn(GunParent, this);
            
            //IK not using now

        }

        #endregion
    }
}