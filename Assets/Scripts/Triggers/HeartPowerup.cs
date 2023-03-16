using System;
using UnityEngine;
using UnityEngine.Events;

namespace Triggers
{
    public class HeartPowerup : MonoBehaviour
    {
        [SerializeField] private float rotateVelocity;
        
        
        // Main Methods
        private void Update() => PowerupRotate();
        
        
        
        // Triggers
        private void OnTriggerEnter(Collider collision)
            => PowerupDestroy(collision);
        
        
        // Methods

        private void PowerupRotate()
        {
            var rotation = 
                new Vector3(0, 1, 0)
                    .normalized * rotateVelocity;

            transform.rotation *= Quaternion.Euler(rotation);
        }
        
        private void PowerupDestroy(Component playerCollider)
        {
            var player = playerCollider
                .gameObject.CompareTag("Player");

            if (!player) return;
            
            Destroy(gameObject);            
        }
    }
}