using System;
using Managers;
using UI;
using UnityEngine;

namespace Triggers
{
    public class BusGoal : MonoBehaviour
    {
        [SerializeField] private LayerMask busLayer;
        [SerializeField] private CanvasController canvas;
        private bool _finish;
        
        // Main Methods
        
        private void Start() => _finish = false;

        private void Update() => DistanceBus();
        


        // Collider Methods
        
        private void OnTriggerExit(Collider busCollider)
        {
            var bus = busCollider.GetComponent<Bus>();
            
            if (!bus) return;
            
            var score = GameManager.Instance.GetScore();
            var health = decimal.Round
                ((decimal)GameManager.Instance.GetPlayerHealth(),2);
            var total = health * (score == 0 ? 1 : score);
            var totalRounded = decimal.Round
                ((decimal)total, 0); 
            
            if(!_finish) canvas.SetFinalScore(score, health, totalRounded);
            if (!_finish) _finish = true;
        }
        
        // Methods

        private float GetDistanceToGoal()
        {
            Physics.Raycast(
                transform.position,
                transform.forward,
                out var busTarget, 
                75f, busLayer);

            return busTarget.distance;
        }

        private void DistanceBus()
        {
            var distance = decimal
                .Round((decimal)GetDistanceToGoal() * 6);
            
            canvas.SetBusGoal("Distancia del bus a la meta " + 
                              distance + " mts.");
            
            switch (distance)
            {
                case > 150:
                    canvas.SetBusGoalColor(new Color(0,255,0));
                    canvas.SetLegendBackground(new Color(0,255,0,0.05f));
                    break;
                case > 00 and <= 150:
                    canvas.SetBusGoalColor(new Color(255,255,0));
                    canvas.SetLegendBackground(new Color(255,255,0,0.05f));
                    break;
                default:
                    canvas.SetBusGoalColor(new Color(255,0,0));
                    canvas.SetLegendBackground(new Color(255,0,0,0.05f));
                    break;
            }

                
        }
    }
}