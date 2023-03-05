using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private TMP_Text powerups;
        [SerializeField] private TMP_Text energyMeter;
        [SerializeField] private TMP_Text powerupMeter;
        [SerializeField] private Image legendBackground;
        [SerializeField] private Image powerupIcon;
        [SerializeField] private Image energyIcon;
        [SerializeField] private TMP_Text busGoal;
        [SerializeField] private TMP_Text legend;
        [SerializeField] private TMP_Text energyNumber;
        [SerializeField] private TMP_Text scoreNumber;
        [SerializeField] private TMP_Text ptsNumber;
        [SerializeField] private GameObject scorePanel;
        private float _legendColorAlpha;
        private Color _legendColor;
        private float _health;


        private void Awake() => _legendColorAlpha = 1;

        private void Start() => SetStartValues();
    


        private void Update()
        {
            var health = GameManager.Instance.GetPlayerHealth();
            var powerups = GameManager.Instance.GetPowerUps();
        
            energyMeter.text = health.ToString(health == 100 ? "": "0.00") + "%";
            powerupMeter.text = powerups.Count.ToString();
        
            RotateIcons();
            PowerupsText();
            DamageReactionText();
            SetLegendTransparency();
        }

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

        private void SetStartValues () => 
            _health = GameManager.Instance.GetPlayerHealth();

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

        public void SetFinalScore(int score, decimal health, decimal total)
        {
            scorePanel.SetActive(true);
            ptsNumber.text = score.ToString();
            energyNumber.text = health.ToString();
            scoreNumber.text = total.ToString();
        }
    }
}
