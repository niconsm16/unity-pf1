using Timer = MainMenu.Scripts.Utilities.Timer;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;
using MainMenu.Scripts.Utilities;
using MainMenu.Scripts.Enums;
using UnityEngine;
using TMPro;

namespace MainMenu.Scripts
{
    public class Menu : MonoBehaviour
    {
        
        [Header("Background Image")]
        [SerializeField] private GameObject screen;
        
        [Header("Time Display (in seconds)")]
        [SerializeField] private float timeDisplay;

        [Space(5)]
        [SerializeField] private TextMeshProUGUI[] texts;
        
        [Space(2)]
        [SerializeField] private Button[] buttons;
        
        [Space(5)]
        [Header("Items")]
        [SerializeField] private GameObject enterLegend;

        [Space(5)] 
        [Header("Audio")] 
        [SerializeField] private GameObject enterAudio;
        [SerializeField] private AudioSource bgmAudio;
        
        private float _count;
        private float _countDown;
        
        private bool _fadeOut;
        private Image _screen;
        private AudioSource _enterAudioSource;
        
        private DifficultLevels _difficult;
        private bool _goSelectLevel;
        
        
        
        // Main Methods
        
        private void Start() => SetInitialValues();
        
        
        private void Update()
        {
            FadeIn();
            FadeOut();
            EnterInputHandler();
            AudioHandler(bgmAudio);
            AudioHandler(_enterAudioSource);
            GoLevel();
        }
        
        
        // Custom Methods
        private void SetInitialValues() 
        {
            _count = 0;
            _countDown = 0;
            
            _fadeOut = false;
            _screen = screen.GetComponent<Image>();
            _enterAudioSource = enterAudio.GetComponent<AudioSource>();

            _difficult = DifficultLevels.Empty;
        }
        
        
        
        
        public void ConfigDifficult(int difficult) =>
            _difficult = SetDifficult(difficult);
        
        private DifficultLevels SetDifficult(int difficult)
        {
            return difficult switch
            {
                1 => DifficultLevels.Easy,
                2 => DifficultLevels.Normal,
                3 => DifficultLevels.Hard,
                _ => DifficultLevels.Empty
            };
        }
        
        private void GoLevel()
        {
            if (!_goSelectLevel || !(_countDown < 0)) return;
            
            switch(_difficult)
            {
                case DifficultLevels.Easy:
                    SceneManager.LoadScene("EasyLevel");
                    break;
                case DifficultLevels.Normal:
                    SceneManager.LoadScene("NormalLevel");
                    break;
                case DifficultLevels.Hard:
                    SceneManager.LoadScene("HardLevel");
                    break;
                case DifficultLevels.Empty: default:
                    break;
            }
        }




        private void FadeIn()
        {
            var stillTransparent = _count <= timeDisplay;

            if (!stillTransparent) return;
            
            _count = Timer.TimerIncrease(_count);
            Effects.Fader(_screen, _count, timeDisplay);
            Effects.Fader(texts, _count, timeDisplay);
        }

        
        
        
        private void FadeOut() 
        {
            if (!_fadeOut) return;
            
            var stillTransparent = _count <= timeDisplay;
            
            if (!stillTransparent && _countDown == 0)
                _countDown = timeDisplay;
            
            if (_countDown <= 0) return;
            
            _countDown = Timer.TimerDecrease(_countDown);
            Effects.Fader(texts, _countDown, timeDisplay);
            Effects.Fader(_screen, _countDown, timeDisplay);
            Effects.Fader(buttons, _countDown, timeDisplay);
        }
        
        
        
        
        // Handlers
        private void EnterInputHandler()
        {
            if (_fadeOut || (
                !Input.GetKeyDown(KeyCode.KeypadEnter) &&
                !Input.GetKeyDown(KeyCode.Return)) ||
                _difficult == DifficultLevels.Empty ||
                Time.time < (timeDisplay / 2)) return;
            
            _fadeOut = true;
            _goSelectLevel = true;
            _enterAudioSource.Play();
            Destroy(enterLegend);
        }
        
        
        
        
        private void AudioHandler(AudioSource audioSource)
        {
            if(_fadeOut && audioSource.isPlaying && _countDown >= 0) 
                Effects.Fader(audioSource, _countDown, timeDisplay);
        }
    }
}