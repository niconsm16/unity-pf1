using System;
using System.Collections.Generic;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace Triggers
{
    public class HeartPowerup : MonoBehaviour
    {
        [SerializeField] private float rotateVelocity;
        
        // Main Methods
        private void Update()
        {
            var rotation = 
                new Vector3(0, 1, 0)
                    .normalized * rotateVelocity;

            transform.rotation *= Quaternion.Euler(rotation);
        }
        
        
        // Triggers
        private void OnTriggerEnter(Collider playerCollider)
        {
            var player = playerCollider
                .gameObject.CompareTag("Player");
            
            if (player) Destroy(gameObject);
        }
    }
}