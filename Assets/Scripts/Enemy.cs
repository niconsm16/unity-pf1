    using Actions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private const float Speed = 1;
    public Transform target;
    private int _action;

    // Constructor
    public Enemy(Transform target)
    { this.target = target; }
    
    // Methods

    private void Start()
    {
        _action = Random.Range(0, 6);
        Animations.Enemy(animator, _action);
    }

    private void Update()
    { Movements.Enemy(target, transform, Speed, _action); }
}
