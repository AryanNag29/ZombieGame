using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region References

    [SerializeField] private Transform PlayerPos;

    #endregion

    private void Start()
    {
        transform.position = PlayerPos.position;
    }

    private void Update()
    {
        transform.position = PlayerPos.position;
    }
}
