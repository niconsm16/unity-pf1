using Managers;
using UnityEngine;

namespace Triggers
{
    public class BusGoal : MonoBehaviour
    {
        [SerializeField] private LayerMask busLayer;
        private bool _finish;
        
        // Main Methods

        private void Start() => _finish = false;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Backspace))
                Debug.Log
                ("Distancia del bus a la meta " + 
                 $"{decimal.Round((decimal)GetDistanceToGoal() * 6)}" + 
                 " mts.");
        }

        // Collider Methods
        
        private void OnTriggerExit(Collider busCollider)
        {
            var bus = busCollider.GetComponent<Bus>();
            
            if (!bus) return;
            
            var score = GameManager.Instance.GetScore();
            var health = GameManager.Instance.GetPlayerHealth();
            var total = health * (score == 0 ? 1 : score);
            var totalRounded = decimal.Round
                ((decimal)total, 0); 
            
            if(!_finish) Debug.Log("SCORE: " + score);
            if(!_finish) Debug.Log("ENERGIA: " + health);
            if(!_finish) Debug.Log
                ("TU PUNTAJE TOTAL ES: " + totalRounded);
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
    }
}