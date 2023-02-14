using UnityEngine;

namespace Triggers
{
    public class BusGoal : MonoBehaviour
    {

        private void OnTriggerExit(Collider busCollider)
        {
            var bus = busCollider.GetComponent<Bus>();
            if (bus != null) Destroy(bus,25f);
        }
    }
}