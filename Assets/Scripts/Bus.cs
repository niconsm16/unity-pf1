using System;
using Actions;
using Managers;
using UnityEngine;

public class Bus : MonoBehaviour
{

    [SerializeField] private float busSpeed;
    [SerializeField] private int keepTouchPoints;
    [SerializeField] private int firstTouchPoints;
    [SerializeField] private int normalTouchPoints;
    [SerializeField] private int longKeepTouchPoints;
    [SerializeField] private int superLongKeepTouchPoints;
    [SerializeField] private GameManager gameManager;
    private float _currentTime;
    private bool _keepTouchPoints;
    private bool _normalTouchPoints;
    private bool _longKeepTouchPoints;
    private bool _firstTouchPoints = true;
    private bool _superLongKeepTouchPoints;

    
    
    // Events
    public event Action<int> OnCollisionScore;
    private void OnCollisionScoreHandler(int touchPoints) =>
        OnCollisionScore?.Invoke(touchPoints);
    
    
    
    // Main Methods
    
    private void Awake() => GameManager.Instance.SetScore(0);

    private void Start() => ResetValues();

    private void Update() => Movements.Bus(busSpeed,transform);

    // Collisions
    
    private void OnCollisionEnter(Collision playerCollider)
    {
        var touch = playerCollider.gameObject.CompareTag("Player");

        if(touch) ShowScore();
        if(touch) _currentTime = Time.time;
        
        if (touch) OnCollisionScoreHandler(_firstTouchPoints 
            ? firstTouchPoints : normalTouchPoints);
        // if (touch) GameManager.Instance.SetScore

        if (touch && _firstTouchPoints) _firstTouchPoints = false;
        if (touch && !_normalTouchPoints) _normalTouchPoints = true;
    }


    private void OnCollisionStay(Collision playerCollider)
    {
        var touch = playerCollider.gameObject.CompareTag("Player");
        
        if(touch) ShowScore();
        
        var littleMoment = touch &&
            Time.time > _currentTime + 3f && 
            Time.time <= _currentTime + 6f &&
            !_keepTouchPoints;

        var longMoment = touch &&
            Time.time > _currentTime + 6f && 
            Time.time <= _currentTime + 10f &&
            !_longKeepTouchPoints;

        var superLongMoment = 
            touch && !_superLongKeepTouchPoints &&
            Time.time > _currentTime + 10f ;
        
        if(littleMoment) GameManager.Instance.SetScore(keepTouchPoints);
        if (littleMoment) _keepTouchPoints = true;
        
        if(longMoment) GameManager.Instance.SetScore(longKeepTouchPoints);
        if (longMoment) _longKeepTouchPoints = true;

        if (superLongMoment) GameManager.Instance.SetScore(superLongKeepTouchPoints);
        if (superLongMoment) _superLongKeepTouchPoints = true;
    }

    private void OnCollisionExit(Collision playerCollider)
    {
        var touch = playerCollider.gameObject.CompareTag("Player");
        if(touch) ResetValues();
    }
        

    // Private Methods
    
    private void ResetValues() => _keepTouchPoints = _normalTouchPoints = 
            _longKeepTouchPoints = _superLongKeepTouchPoints = false;
    
    private static void ShowScore() => 
        Debug.Log(GameManager.Instance.GetScore());
}
