using UnityEngine;

public class CityLights : MonoBehaviour
{
    [SerializeField] private GameObject sun;
    [SerializeField] private Material[] materialLights;

    private void Start()
    {
        var sunAngle = sun.transform.eulerAngles.x;
        var day = sunAngle > 9.5 && sunAngle < 168;
        var night = sunAngle is < 0 or > 180;

        if (day) DeactivateLights();
        else ActivateLights();
        
        sun.GetComponent<Light>().enabled = !night;
        
        var poleLights = GameObject
            .FindGameObjectsWithTag("PoleTrafficLight");
        
        foreach(var poleLight in poleLights)
            poleLight.GetComponent<Light>().enabled = !day;
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
    
}
