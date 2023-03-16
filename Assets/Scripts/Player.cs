using System;
using UnityEngine;
using Managers;
using Actions;
using UI;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class Player : MonoBehaviour
{
    private static readonly int Push = Animator.StringToHash("Push");
    
    [SerializeField] private Volume egoVolume;
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
    private bool _egoMode;
    private bool _push;

    
    // Events
    public UnityEvent<float> onTouch;
    public event Action<bool> OnPowerup;
    public event Action<float, bool> OnDamage;
    private void OnTouchHandler(float time)
        => onTouch?.Invoke(time);
    private void OnPowerupHandler(bool toAdd)
        => OnPowerup?.Invoke(toAdd);
    private void OnDamageHandler(float actualDamage, bool isDamage)
        => OnDamage?.Invoke(actualDamage, isDamage); 



    // Main Methods
    private void Awake() => SetHealth();

    private void Start() => SetPlayer();

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
            canvas, 
            EgoMode,
            EgoModeIncrement);
    }
    
    
    
    
    // Collisions

    private void OnCollisionEnter(Collision collision)
    {
        EnemyFirstCollide(collision);
    }
    
    private void OnCollisionStay(Collision collision)
    {
      EnemyStayCollide(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
       EnemyExitCollide(collision);
    }

    
    
    
    // Triggers
    
    private void OnTriggerEnter(Collider trigger)
    {
        GetPowerup(trigger);
    }



    
    // Methods

    public float GetHealth() => health;

    private void SetHealth()
    {
        GameManager.Instance.SetPlayerHealth(health);
        _initialHealth = health;
    }

    private void SetPlayer()
    {
        _egoMode = false;
        _currentTime = Time.time;
        egoVolume.gameObject.SetActive(_egoMode);
    }

    // // Enemies Collisions
    private void EnemyFirstCollide(Collision enemyCollider)
    {
        var metrobus = enemyCollider.gameObject.CompareTag("Metrobus");
        var enemy = enemyCollider.gameObject.CompareTag("Enemy"); 
        var obstacles = enemy || metrobus;
        
        _damageTimer = Time.time + 0.2f;
        
        if (enemy) OnDamageHandler(damage, true);
 
        if (obstacles) _timeout = Time.time;
        if (obstacles) _push = false;
    }

    private void EnemyStayCollide(Collision enemyCollider)
    {
        var obstacles = 
            enemyCollider.gameObject.CompareTag("Enemy");
        
        if (obstacles) _push = true;

        if (obstacles && Time.time > _timeout + 0.3f)
            animator.SetBool(Push, true);
        if (obstacles && Time.time > _damageTimer)
            OnDamageHandler(lowDamage, true);
        if (obstacles && Time.time > _damageTimer) 
            _damageTimer += 0.1f;
    }

    private void EnemyExitCollide(Collision enemyCollider)
    {
        var obstacles = 
            enemyCollider.gameObject.CompareTag("Enemy");
        
        if (obstacles) animator.SetBool(Push, false);
        if (obstacles) _push = false;
    }
    
    
    // // Powerups
    private void GetPowerup(Component powerupTrigger)
    {
        var powerup = powerupTrigger.gameObject.CompareTag("Powerup");
        if (!powerup) return;
        
        OnPowerupHandler(true);
        var timeNow = Time.time;
        OnTouchHandler(timeNow);
    } 
    
    // // Volume
    private void EgoMode()
    {
        egoVolume.gameObject.SetActive(!_egoMode);
        _egoMode = !_egoMode;
    }

    private void EgoModeIncrement()
    {
        var profile = egoVolume.profile;
        profile.TryGet(out Bloom _bloom);
        _bloom.threshold.value -= 0.05f;
    }

}

