using ScriptableObjects;
using UnityEngine;

namespace Entities
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected FanEnemyValues fanEnemyValues;
        [SerializeField] protected int animationsCount;
        [SerializeField] protected Animator animator;
        public Transform target;
        protected float Speed;
        protected int Action;

        protected void EnemyInit(int maxActions, float speed)
        {
            Action = Random.Range(0, maxActions);
            Speed = speed;
        } 
    }
}