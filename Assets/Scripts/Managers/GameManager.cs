using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static float _playerHealth;
    private static int _totalScore;
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public static int GetScore() => _totalScore;
    public static void SetScore(int points) => _totalScore += points;

    public static float GetPlayerHealth() => _playerHealth;
    
    public static void SetPlayerHealth(float health) 
        => _playerHealth = health;
    public static void SetPlayerDamage(float health, bool damage) 
        => _playerHealth += damage ? -(health) : health;
}   
}
