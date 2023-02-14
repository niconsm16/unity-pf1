using System;
using Actions;
using Unity.VisualScripting;
using UnityEngine;

public class Bus : MonoBehaviour
{

    [SerializeField] private float busSpeed;

    private void Update() => Movements.Bus(busSpeed,transform);

    private void OnCollisionEnter(Collision playerCollider)
    {
        if (playerCollider.gameObject.tag == "Player") 
            Debug.Log("GANASTEEEE!");
    }
}
