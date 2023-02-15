using Actions;
using Managers;
using UnityEngine;

public class Bus : MonoBehaviour
{

    [SerializeField] private float busSpeed;
    [SerializeField] private int keepTouch;
    [SerializeField] private int firstTouch;
    [SerializeField] private int normalTouch;
    [SerializeField] private int longKeepTouch;
    [SerializeField] private int superLongKeepTouch;
    private float _currentTime;
    private bool _keepTouch;
    private bool _normalTouch;
    private bool _longKeepTouch;
    private bool _firstTouch = true;
    private bool _superLongKeepTouch;

    // Main Methods
    
    private void Awake() => GameManager.SetScore(0);

    private void Start() => ResetValues();

    private void Update() => Movements.Bus(busSpeed,transform);

    // Collisions
    
    private void OnCollisionEnter(Collision playerCollider)
    {
        var touch = playerCollider.gameObject.CompareTag("Player");
        
        if(touch) ShowScore();
        if(touch) _currentTime = Time.time;
        
        if (touch) GameManager.SetScore
            (_firstTouch ? firstTouch : normalTouch);

        if (touch && _firstTouch) _firstTouch = false;
        if (touch && !_normalTouch) _normalTouch = true;
    }


    private void OnCollisionStay(Collision playerCollider)
    {
        var touch = playerCollider.gameObject.CompareTag("Player");
        
        if(touch) ShowScore();
        
        var littleMoment = touch &&
            Time.time > _currentTime + 3f && 
            Time.time <= _currentTime + 6f &&
            !_keepTouch;

        var longMoment = touch &&
            Time.time > _currentTime + 6f && 
            Time.time <= _currentTime + 10f &&
            !_longKeepTouch;

        var superLongMoment = 
            touch && !_superLongKeepTouch &&
            Time.time > _currentTime + 10f ;
        
        if(littleMoment) GameManager.SetScore(keepTouch);
        if (littleMoment) _keepTouch = true;
        
        if(longMoment) GameManager.SetScore(longKeepTouch);
        if (longMoment) _longKeepTouch = true;

        if (superLongMoment) GameManager.SetScore(superLongKeepTouch);
        if (superLongMoment) _superLongKeepTouch = true;
    }

    private void OnCollisionExit(Collision playerCollider)
    {
        var touch = playerCollider.gameObject.CompareTag("Player");
        if(touch) ResetValues();
    }
        

    // Private Methods
    
    private void ResetValues() => _keepTouch = _normalTouch = 
            _longKeepTouch = _superLongKeepTouch = false;
    
    private static void ShowScore() => 
        Debug.Log(GameManager.GetScore());
}
