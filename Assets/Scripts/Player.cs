using Actions;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speedVelocity;
    [SerializeField] private float speedRotation;
    [SerializeField] private Animator animator;
    private float _currentTime;

    private void awake()
    {
        _currentTime = Time.time;
    }
    
    private void Update()
    {
        if (!(Time.time >= (_currentTime + 4f))) return;
        Controllers.Player(speedVelocity, speedRotation, transform);
        Animations.Player(animator);
    }

}
