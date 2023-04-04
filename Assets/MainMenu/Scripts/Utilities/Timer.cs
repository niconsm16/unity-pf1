using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu.Scripts.Utilities
{


    public class Timer : MonoBehaviour
    {
        public static float TimerRepeater(float limit, float count) =>
            count > limit ? 0 : count + Time.deltaTime;

        
        public static float TimerIncrease(float count) => 
            count + Time.deltaTime;

        
        public static float TimerDecrease (float count) => 
            count - Time.deltaTime;
    }
}