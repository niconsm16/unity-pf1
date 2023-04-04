using UnityEngine;

namespace MainMenu.Scripts.Utilities
{
    public class Utilities : MonoBehaviour
    {
        protected static float OscillationValue(float currentValue, float maxValue)
        {
            var unit = maxValue / 100;
            var percent = currentValue / unit;

            var upward = (percent / 100) * 2;
            var falling = 2 - upward;

            return upward <= 1 ? upward : falling;
        }

        protected static float ChangeValue(float currentValue, float maxValue)
        {
            var unit = maxValue / 100;
            var percent = currentValue / unit;
            var coefficient = percent / 100;
            return coefficient;
        }
    }
}