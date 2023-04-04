using MainMenu.Scripts.Utilities;
using UnityEngine;
using TMPro;

namespace MainMenu.Scripts
{
    public class EnterScript : MonoBehaviour
    {
        [SerializeField] private float blinkSpeed;
        private float _count;
        private float _maxOpacity;
        private TextMeshProUGUI _text;
        

        // Main Methods
        
        private void Start() => SetInitialValues();
        
        private void Update()
        {
            _count = Timer.TimerRepeater(blinkSpeed, _count);
            SetText();
        }
        
        
        
        // Custom Methods

        private void SetInitialValues() {
            _count = 0;
            _text = gameObject.GetComponent<TextMeshProUGUI>();
        }

        
        
        
        private void SetText() => 
            Effects.Blink (_text, _count, blinkSpeed);
    }
}