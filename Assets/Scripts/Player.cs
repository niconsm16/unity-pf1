using UnityEngine;
using Managers;
using Actions;
using Enums;
using UI;


public class Player : MonoBehaviour
{
    private static readonly int Push = Animator.StringToHash("Push");
    
    [SerializeField] private CanvasController canvas;
    [SerializeField] private float speedVelocity;
    [SerializeField] private float speedRotation;
    [SerializeField] private LayerMask busLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private float lowDamage;
    [SerializeField] private float damage;
    [SerializeField] private float health;
    
    private float _initialHealth;
    private float _currentTime;
    private float _damageTimer;
    private float _timeout;
    private bool _push;

    
    // Main Methods
    
    private void Awake() => _currentTime = Time.time;

    private void Start() => SetHealth();

    private void Update()
    {
        if (!(Time.time >= (_currentTime + 4f))) return;
        
        Animations.Player(animator);
        Controllers.Player(
            _push ? speedVelocity * 0.3f : speedVelocity, 
            speedRotation,
            transform,
            busLayer, 
            _initialHealth,
            canvas);
    }
    
    
    // Collisions

    private void OnCollisionEnter(Collision enemyCollider)
    {
        var metrobus = enemyCollider.gameObject.CompareTag("Metrobus");
        var enemy = enemyCollider.gameObject.CompareTag("Enemy"); 
        var obstacles = enemy || metrobus;
        
        _damageTimer = Time.time + 0.2f;
        
        if (enemy) GameManager.Instance.SetPlayerDamage(damage, true);
        if (obstacles) _timeout = Time.time;
        if (obstacles) _push = false;
    }
    
    private void OnCollisionStay(Collision enemyCollider)
    {
        var obstacles = 
            enemyCollider.gameObject.CompareTag("Enemy");
        
        if (obstacles) _push = true;
        
        if(obstacles) Debug.Log
            (GameManager.Instance.GetPlayerHealth());
        
        if (obstacles && Time.time > _timeout + 0.3f)
            animator.SetBool(Push, true);
        if (obstacles && Time.time > _damageTimer)
            GameManager.Instance.SetPlayerDamage(lowDamage, true);
        if (obstacles && Time.time > _damageTimer) 
            _damageTimer += 0.1f;
    }

    private void OnCollisionExit(Collision enemyCollider)
    {
        var obstacles = 
            enemyCollider.gameObject.CompareTag("Enemy");
        
        if (obstacles) animator.SetBool(Push, false);
        if (obstacles) _push = false;
    }

    private void OnTriggerEnter(Collider onTrigger)
    {
        var powerup = onTrigger.gameObject.CompareTag("Powerup");
        if (powerup) GameManager.Instance.SetPowerUps(true);
    }

    // Methods

    private void SetHealth()
    {
        GameManager.Instance.SetPlayerHealth(health);
        _initialHealth = health;
    }
}
