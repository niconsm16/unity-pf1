using System;
using Managers;
using UnityEngine;

namespace Triggers
{
    public class BusGoal : MonoBehaviour
    {
        [SerializeField] private LayerMask busLayer;
        private bool finish = false;
        
        // Main Methods

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Backspace))
                Debug.Log
                ("Distancia del bus a la meta " + 
                 $"{decimal.Round((decimal)getDistanceToGoal() * 6)}" + 
                 " mts.");
        }

        // Collider Methods
        
        private void OnTriggerExit(Collider busCollider)
        {
            var score = GameManager.GetScore();
            var bus = busCollider.GetComponent<Bus>();
            var health = GameManager.GetPlayerHealth();
            var total = health * (score == 0 ? 1 : score);
            var totalRounded = decimal.Round
                ((decimal)total, 0); 
            
            if (bus != null) Destroy(bus,25f);
            if(!finish) Debug.Log("SCORE: " + score);
            if(!finish) Debug.Log("ENERGIA: " + health);
            if(!finish) Debug.Log
                ("TU PUNTAJE TOTAL ES: " + totalRounded);
            if (!finish) finish = true;
        }
        
        // Methods

        public float getDistanceToGoal()
        {
            Physics.Raycast(
                transform.position,
                transform.forward,
                out var busTarget,
                75f, busLayer
            );

            return busTarget.distance;
        }
    }
}