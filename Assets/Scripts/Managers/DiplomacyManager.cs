using UnityEngine;

namespace Managers
{
    public class DiplomacyManager : MonoBehaviour
    {
        public static DiplomacyManager Instance;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }
    }
}