using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "HeartPowerupValues",
    menuName = "Scriptable Objects/Powerup/Heart Powerup Values")]
public class HeartPowerupValues : ScriptableObject
{
    public int powerupFirstPercent;
    public int powerupSecondPercent;
    public int powerupThirdPercent;
    
    public Dictionary<string, float> _powerupLevels;
}
