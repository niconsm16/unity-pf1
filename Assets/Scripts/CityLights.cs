using System;
using UnityEngine;

public class CityLights : MonoBehaviour
{
    [SerializeField] private GameObject sun;
    [SerializeField] private Material[] materialLights;
    private bool _activated;
    private Light _sun;
    private GameObject[] _poleTrafficLights;
    private GameObject[] _obeliscoLights;

    private void Awake()
    {
        _activated = false;
        _sun = sun.GetComponent<Light>();
        _poleTrafficLights = GameObject.FindGameObjectsWithTag("PoleTrafficLight");
        _obeliscoLights = GameObject.FindGameObjectsWithTag("Obelisco Light");
    }


    private void Update()
    {
        _activated = true;
    }
    
    private void FixedUpdate()
    {
       SetInfoLights();
    }

    private void SetInfoLights()
    {
        if (_activated) return;
        
        var sunAngle = sun.transform.eulerAngles.x;
        var day = sunAngle > 9.5 && sunAngle < 168;
        var night = sunAngle is < 0 or > 180;

        if (day) DeactivateLights();
        else ActivateLights();
        
        _sun.enabled = !night;
        
        TurnLights(_poleTrafficLights, day);
        TurnLights(_obeliscoLights, day);
    }

    private void ActivateLights()
    {
        foreach (var material in materialLights)
            material.EnableKeyword("_EMISSION");
    }

    
    private void DeactivateLights()
    {
        foreach (var material in materialLights)
            material.DisableKeyword("_EMISSION");
    }
    

    private static void TurnLights(GameObject[] arrayLights, bool day)
    {
        foreach(var light in arrayLights)
            light.GetComponent<Light>().enabled = !day;
    }
    
}
