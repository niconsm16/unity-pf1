using Managers;
using UnityEngine;

namespace Triggers
{
    public class BusGoal : MonoBehaviour
    {
        private bool finish = false;
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
    }
}