using UnityEngine;
using Helpers;
using Triggers;
using Unity.VisualScripting;
using UnityEngine.Events;

namespace Managers
{
    public class CharactersManager : MonoBehaviour
    {
        [Header("Enemies Generator")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Material[] enemyShirts;
        [SerializeField] private Material[] enemyHairs;
        [SerializeField] private Material[] enemyPants;
        [SerializeField] private Material[] enemyShoes;
        [SerializeField] private Transform enemyPointA;
        [SerializeField] private Transform enemyPointB;
        [SerializeField] private Transform enemyTarget;
        [SerializeField] private int enemiesQuantity;

        [Space(10)]
        [Header("Powerups Generator")]
        [SerializeField] private GameObject powerUpPrefab;
        [SerializeField] private Transform powerupPointA;
        [SerializeField] private Transform powerupPointB;
        [SerializeField] private int powerupsQuantity;

        public static CharactersManager Instance;
        
        
        // Main Methods

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }
        
        private void Start()
        {
            GenerateEnemies();
            GeneratePowerups();
        }

        
        // Generate Methods
        
        private void GenerateEnemies()
        {
            for (var i = 0; i <= enemiesQuantity; i++)
            {
                var enemy = enemyPrefab.GetComponent<FanEnemy>();
                enemy.target = enemyTarget;
                
                Instantiate(enemy, 
                    Arbitrary.Position(enemyPointA, enemyPointB), 
                    Quaternion.identity);
            }
            Arbitrary.Materials("ShirtColor", enemyShirts);
            Arbitrary.Materials("HairColor", enemyHairs);
            Arbitrary.Materials("PantsColor", enemyPants);
            Arbitrary.Materials("ShoesColor", enemyShoes);
        }

        private void GeneratePowerups()
        {
            for (var i = 0; i < powerupsQuantity; i++)
            {
                var powerup = powerUpPrefab.GetComponent<HeartPowerup>(); ;
                Instantiate(powerup, 
                    Arbitrary.Position(powerupPointA, powerupPointB), 
                    Quaternion.identity);
            }
        }
    }
}