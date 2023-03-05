using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(
        fileName = "FanEnemyValues", 
        menuName = "Scriptable Objects/Enemy/Fan Enemy Values")]
    
    public class FanEnemyValues : ScriptableObject
    {
        public int fanEnemyMaxActions;
        public float fanEnemySpeed;
    }
}