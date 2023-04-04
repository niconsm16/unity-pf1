using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace MainMenu.Scripts.Utilities
{
    public class Effects : Utilities
    {
        public static void Fader(Image image, float count, float maxValue)
        {
            var color = image.color;
            var opacity = ChangeValue(count, maxValue);
            image.color = new Color(color.r, color.g, color.b, opacity);
        }
        
        public static void Fader(AudioSource audio, float count, float maxValue)
        {
            var audioLevel = ChangeValue(count, maxValue);
            audio.volume = audioLevel <= 0 ? 0 : audioLevel;
        }
        
        public static void Fader(IEnumerable<TextMeshProUGUI> texts, float count, float maxValue)
        {
            foreach (var text in texts)
            {
                var color = text.color;
                var opacity = ChangeValue(count, maxValue);
                text.color = new Color(color.r, color.g, color.b, opacity);
            }
        }
        
        public static void Fader(IEnumerable<Button> buttons,float count, float maxValue)
        {
            foreach (var button in buttons)
            {
                var sizes = button.transform.localScale;
                var opacity = ChangeValue(count, maxValue);
                button.transform.localScale = new Vector3(opacity,sizes.y,sizes.z);
            }
        }

        
        

        
        
        public static void Blink(TextMeshProUGUI text, float count, float limit)
        {
            var color = text.color;
            var opacity = OscillationValue(count, limit);
            text.color = new Color(color.r, color.g, color.b, opacity);
        }
    }
}