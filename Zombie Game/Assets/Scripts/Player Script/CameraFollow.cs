using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region References

    [SerializeField] private Transform PlayerPos;

    #endregion

    private void Start()
    {
        //start follow
        transform.position = new Vector3(PlayerPos.position.x, 1 , PlayerPos.position.z);
    }

    private void Update()
    {
        transform.position = new Vector3(PlayerPos.position.x, 1 , PlayerPos.position.z);
    }
}
