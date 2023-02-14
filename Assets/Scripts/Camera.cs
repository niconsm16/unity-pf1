using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Cinemachine;
using UnityEngine;
using Actions;

public class Camera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    private void LateUpdate()
    {
        Controllers.Camera(cameras);
    }
}
