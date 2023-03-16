using UnityEngine.Events;
using UnityEngine;
using Managers;
using UI;

namespace Triggers
{
    public class BusGoal : MonoBehaviour
    {
        [SerializeField] private CanvasController canvas;
        [SerializeField] private LayerMask busLayer;
        private bool _finish;

        // Events
        public UnityEvent<int, decimal, decimal> onGoal;
        private void OnGoalHandler(int score, decimal health, decimal totalRounded) 
            => onGoal?.Invoke(score, health, totalRounded);
        
        
        // Main Methods
        
        private void Start() => _finish = false;

        private void Update() => DistanceBus();
        


        // Collider Methods
        
        private void OnTriggerExit(Collider collision)
            => BusOnGoal(collision);
        

        
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
        
        private void BusOnGoal (Component busCollider)
        {
            var bus = busCollider.GetComponent<Bus>();
        
            if (!bus) return;
        
            var score = GameManager.Instance.GetScore();
            var health = decimal.Round
                ((decimal)GameManager.Instance.GetPlayerHealth(),2);
            var total = health * (score == 0 ? 1 : score);
            var totalRounded = decimal.Round
                (total, 0); 
        
            if(!_finish) OnGoalHandler(score, health, totalRounded);
            if (!_finish) _finish = true;            
        }                
    }
}