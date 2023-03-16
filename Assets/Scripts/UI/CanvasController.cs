using Managers;
using TMPro;
using Triggers;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {
        // For Events
        [Header("For Events")] 
        private float _hpuTimeToClose;
        [SerializeField] private float powerupTimeToClose;
        [SerializeField] private Player player;
        [SerializeField] private BusGoal busGoalObject;
        [SerializeField] private GameManager gameManager;
        
        // Health & Powerup Panels (Inferior)
        private float _health;
        [Space(10)]
        [Header("Inferior Panel (Health & Powerups")]
        [SerializeField] private Image energyIcon;
        [SerializeField] private Image powerupIcon;
        [SerializeField] private TMP_Text powerups;
        [SerializeField] private TMP_Text energyMeter;
        [SerializeField] private TMP_Text powerupMeter;

        // Legends Panel (Superior)
        private Color _legendColor;
        private float _legendColorAlpha;
        [Space(10)]
        [Header("Superior Panel (Legends)")]
        [SerializeField] private TMP_Text legend;
        [SerializeField] private TMP_Text busGoal;
        [SerializeField] private Image legendBackground;
        
        // Score Goal Panel
        [Space(10)]
        [Header("Goal (Score) Panel")]
        [SerializeField] private TMP_Text ptsNumber;
        [SerializeField] private TMP_Text scoreNumber;
        [SerializeField] private TMP_Text energyNumber;
        [SerializeField] private GameObject scorePanel;
        
        // Death Panel
        [Space(10)] 
        [Header("Death Panel")] 
        [SerializeField] private GameObject deathPanel;
        
        // Heart Powerup Panel
        [SerializeField] private GameObject heartPowerupPanel;
        
        // Main Methods

        private void Awake() => _legendColorAlpha = 1;

        private void Start()
        {
            SetStartValues();
            InitializeEvents();
        }

        private void Update()
        {

            SetPanelValues();
            AutoClosePanel("heart");
            
            RotateIcons();
            PowerupsText();
            DamageReactionText();
            SetLegendTransparency();
        }
        
        
        // Events
        private void InitializeEvents()
        {
            gameManager.onDeath.AddListener(ShowDeathPanel);
            busGoalObject.onGoal.AddListener(ShowScorePanel);
            player.onTouch.AddListener(ShowHeartPowerupPanel);
        }


        // Methods

        private void SetStartValues ()
        {
            _health = GameManager.Instance.GetPlayerHealth();
            _hpuTimeToClose = 0;
        }
        
        // // Set Panels
        private void ShowDeathPanel() => deathPanel.SetActive(true);
        
        private void ShowScorePanel(int score, decimal health, decimal total)
        {
            scorePanel.SetActive(true);
            ptsNumber.text = score.ToString();
            energyNumber.text = health.ToString();
            scoreNumber.text = total.ToString();
        }

        private void ShowHeartPowerupPanel(float timeNow)
            => _hpuTimeToClose = timeNow + powerupTimeToClose;

        private void AutoClosePanel(string powerup)
        {
            if (!powerup.Equals("heart")) return;
            var timeToClose = !(_hpuTimeToClose < Time.time);
            heartPowerupPanel.SetActive(timeToClose);
        }

        private void SetPanelValues()
        {
            var health = GameManager.Instance.GetPlayerHealth();
            var powerups = GameManager.Instance.GetPowerUps();
        
            energyMeter.text = health.ToString(health == 100 ? "": "0.00") + "%";
            powerupMeter.text = powerups.Count.ToString();
        }


        // // Panel: Inferior
        
        private void RotateIcons()
        {
            var yAxis = new Vector3(0, 10, 0);
            energyIcon.transform.Rotate(yAxis);
            powerupIcon.transform.Rotate(yAxis);
        }

        private void DamageReactionText()
        {
            var actualHealth = GameManager.Instance.GetPlayerHealth();
        
            var damage = actualHealth < _health;
            var recovery = actualHealth > _health;
            var fine = actualHealth == _health;

            if (damage) energyMeter.color = new Color(255, 0, 0);
            if (recovery) energyMeter.color = new Color(0, 0, 255);
            if (fine)  energyMeter.color = new Color(255, 255, 255);
        
            if (damage || recovery) _health = actualHealth;
        }

        private void PowerupsText()
        {
            var count = GameManager.Instance.GetPowerUps().Count;
            
            var empty = new Color(0, 0, 0, 0.75f); 
            var light = new Color(255, 255, 255, 0.3f); 
            var medium = new Color(255, 255, 255, 0.4f); 
            var full = new Color(255, 255, 255, 1f); 
            
            powerups.color = count switch
            {
                0 => empty,
                1 => light,
                2 => medium,
                3 => full,
                _ => powerups.color
            };

            powerupMeter.color = count switch
            {
                0 => empty,
                1 => light,
                2 => medium,
                3 => full,
                _ => powerupMeter.color
            };
        }

        // // Panel: Superior

        public void SetLegendText (string actualLegend) 
            => legend.text = actualLegend;
        
        public void SetLegendColor (bool onTarget) 
            => legend.color = onTarget 
                ? new Color(0,255,0,1) 
                : new Color(255,0,0,1);

        public void SetBusGoal(string goalLegend) 
            => busGoal.text = goalLegend;

        public void SetBusGoalColor(Color colorLegend)
            => busGoal.color = colorLegend;

        public void SetLegendBackground(Color background)
            => legendBackground.color = background;

        private void SetLegendTransparency()
        {
            var empty = legend.text == "";
            var isTransparent = _legendColorAlpha <= 0;

            var red = new Color(255, 0, 0, _legendColorAlpha);
            var green = new Color(0, 255, 0, _legendColorAlpha);
            
            if (!empty) _legendColorAlpha -= 0.05f;
            
            if (!empty && legend.color == red)
                legend.color = new Color(255, 0, 0, _legendColorAlpha);
            
            if (!empty && legend.color == green)
                legend.color = new Color(0, 255, 0, _legendColorAlpha);

            if (isTransparent) legend.text = "";
            if (isTransparent) _legendColorAlpha = 1;
        }


    }
}
