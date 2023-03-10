using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Enums;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] 
        private HeartPowerupValues heartPowerUp;
        public Countries userCountry;
        public string userName;
        public int userAge;

        public static GameManager Instance;
        
        private Dictionary<string, string> _userData;
        private List<float> _powerups;
        private float _playerHealth;
        private int _totalScore;
        
        
        // Main Methods
        
        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
            
            Instance._userData = new Dictionary<string, string>();
        }

        private void Start() 
        {
            SetPowerUpLevels();
            SetUserData(userName, userAge, userCountry);
        }

        
        
        // Getters & Setters
        
        //Score
        
        public int GetScore() => _totalScore;
        public void SetScore(int points) => _totalScore += points;

        
        
        // Health
        
        public float GetPlayerHealth() => Instance._playerHealth;
        public void SetPlayerHealth(float health)
            => Instance._playerHealth = health;
        public void SetPlayerDamage(float health, bool damage) 
            => Instance._playerHealth += damage ? - health : health;

        
        
        // User Data
        
        public Dictionary<string, string> GetUserData() 
            => Instance._userData;
        private static void SetUserData(
            string name, int age, Countries country)
        {
            if (Instance._userData == null) return;
            
            Instance._userData.Add("Nombre", name);
            Instance._userData.Add("Edad", age.ToString());
            Instance._userData.Add("País", country.ToString());
        }
        
        
        
        //Power Up Levels
        
        public Dictionary<string, float> GetPowerUpLevels() 
            => heartPowerUp._powerupLevels;
        
        private void SetPowerUpLevels()
        {
            float Percent(float maxHealth, int percent) 
                => ( maxHealth / 100 ) * percent;
            
            if (heartPowerUp._powerupLevels != null) return;
            
            heartPowerUp._powerupLevels = new Dictionary<string, float>();
            _powerups = new List<float>();
            
            var maxHealth = Instance.GetPlayerHealth();
            
            heartPowerUp._powerupLevels.Add("First", 
                Percent(maxHealth, heartPowerUp.powerupFirstPercent));
            heartPowerUp._powerupLevels.Add("Second", 
                Percent(maxHealth, heartPowerUp.powerupSecondPercent));
            heartPowerUp._powerupLevels.Add("Third", 
                Percent(maxHealth, heartPowerUp.powerupThirdPercent));
        }
        
        
        
        
        // Power Ups
        public List<float> GetPowerUps() => _powerups;

        public void SetPowerUps(bool toAdd)
        {
            if (toAdd)
                switch (_powerups.Count)
                {
                    case 0:
                        _powerups.Add(heartPowerUp._powerupLevels["First"]);
                        break;
                    case 1:
                        _powerups.Add(heartPowerUp._powerupLevels["Second"]);
                        break;
                    default:
                        _powerups.Add(heartPowerUp._powerupLevels["Third"]);
                        break;
                }
            else _powerups.Remove(_powerups.Last());
        }
        
    }   
}
