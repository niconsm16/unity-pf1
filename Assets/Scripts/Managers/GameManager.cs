using System.Collections.Generic;
using UnityEngine.Events;
using ScriptableObjects;
using UnityEngine;
using System.Linq;
using Enums;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private HeartPowerupValues heartPowerUp;
        
        
        [Space(10)] 
        [Header("User Data")] 
        [SerializeField] private Player player;
        public Countries userCountry;
        public string userName;
        public int userAge;

        public static GameManager Instance;
        
        [Space(10)] 
        [Header("Bus GameObject")] 
        [SerializeField] private Bus bus;
        
        private Dictionary<string, string> _userData;
        private List<float> _powerups;
        private float _playerHealth;
        private int _totalScore;
        
        
        
        
        // Events

        public UnityEvent onDeath;
        private void OnDeathHandler() => onDeath?.Invoke();
        
        
        
        
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
            InitializeEvents();
            SetUserData(userName, userAge, userCountry);
        }


        

        // Events: Init
        
        private void InitializeEvents()
        {
            player.OnPowerup += SetPowerUps;
            player.OnDamage += SetPlayerDamage;
            bus.OnCollisionScore += SetScore;
            
        }
        
        
        
        
        // Getters & Setters
        
        // // Score
        public int GetScore() => _totalScore;
        public void SetScore(int points) => _totalScore += points;

        

        
        // // Health
        
        public float GetPlayerHealth() => Instance._playerHealth;
        
        public void SetPlayerHealth(float health)
            => Instance._playerHealth = health;
        
        public void SetPlayerDamage(float health, bool damage) 
        {
            Instance._playerHealth += damage ? -health : health;
            
            if(Instance._playerHealth <= 0) 
                Instance._playerHealth = 0;
            
            if (Instance._playerHealth == 0)
                OnDeathHandler();
        }




        // // User Data
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
        
        
        
        
        // // Power Up Levels
        public Dictionary<string, float> GetPowerUpLevels() 
            => heartPowerUp.powerupLevels;
        
        private void SetPowerUpLevels()
        {
            float Percent(float maxHealth, int percent) 
                => ( maxHealth / 100 ) * percent;
            
            if (heartPowerUp.powerupLevels != null) return;
            
            heartPowerUp.powerupLevels = new Dictionary<string, float>();
            _powerups = new List<float>();
            
            var maxHealth = Instance.GetPlayerHealth();
            
            heartPowerUp.powerupLevels.Add("First", 
                Percent(maxHealth, heartPowerUp.powerupFirstPercent));
            heartPowerUp.powerupLevels.Add("Second", 
                Percent(maxHealth, heartPowerUp.powerupSecondPercent));
            heartPowerUp.powerupLevels.Add("Third", 
                Percent(maxHealth, heartPowerUp.powerupThirdPercent));
        }
        
        
        
        
        // // Power Ups
        public List<float> GetPowerUps() => _powerups;

        public void SetPowerUps(bool toAdd)
        {
            if (toAdd)
                switch (_powerups.Count)
                {
                    case 0:
                        _powerups.Add(heartPowerUp.powerupLevels["First"]);
                        break;
                    case 1:
                        _powerups.Add(heartPowerUp.powerupLevels["Second"]);
                        break;
                    default:
                        _powerups.Add(heartPowerUp.powerupLevels["Third"]);
                        break;
                }
            else _powerups.Remove(_powerups.Last());
        }
        
    }   
}
