using System.Collections;
using System.Collections.Generic;
using Actions;
using UnityEngine;

public class Bus : MonoBehaviour
{

    [SerializeField] private float busSpeed;
    
    private void Update()
    {
        Movements.Bus(busSpeed,transform);
    }
}
