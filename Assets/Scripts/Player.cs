using Actions;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int Push = Animator.StringToHash("Push");
    [SerializeField] private float speedVelocity;
    [SerializeField] private float speedRotation;
    [SerializeField] private Animator animator;
    private float _currentTime;
    private float _timeout;
    private bool _push;

    private void Awake() => _currentTime = Time.time;

    private void Start() => _push = false;

    private void Update()
    {
        if (!(Time.time >= (_currentTime + 4f))) return;
        Animations.Player(animator);
        Controllers.Player(_push ? speedVelocity * 0.3f : speedVelocity, speedRotation, transform);
    }

    private void OnCollisionEnter(Collision enemyCollider)
    {
        var obstacles = 
            enemyCollider.gameObject.CompareTag("Enemy") ||
            enemyCollider.gameObject.CompareTag("Metrobus");
        
        if (obstacles) _timeout = Time.time;
    }
    
    private void OnCollisionStay(Collision enemyCollider)
    {
        var obstacles = 
            enemyCollider.gameObject.CompareTag("Enemy");

        if (obstacles) _push = true;
  
        if (obstacles && Time.time > _timeout + 0.3f)
            animator.SetBool(Push, true);
    }

    private void OnCollisionExit(Collision enemyCollider)
    {
        var obstacles = 
            enemyCollider.gameObject.CompareTag("Enemy");
        
        if (obstacles) animator.SetBool(Push, false);
        if (obstacles) _push = false;
    }
}
