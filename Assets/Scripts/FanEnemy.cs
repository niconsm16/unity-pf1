    using Actions;
    using Entities;
    using UnityEngine;
    
    public class FanEnemy : Enemy
{

    // Constructor
    public FanEnemy(Transform target)
        => this.target = target; 
    
    // Methods

    private void Awake()
        => EnemyInit(
            fanEnemyValues.fanEnemyMaxActions, 
            fanEnemyValues.fanEnemySpeed);

    private void Start()
    => Animations.Enemy(animator, Action);

    private void Update()
        => Movements.Enemy
            (target, transform, Speed, Action);
}
