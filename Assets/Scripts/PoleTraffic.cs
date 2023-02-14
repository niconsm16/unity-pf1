using System.Collections.Generic;
using UnityEngine;

public class PoleTraffic : MonoBehaviour
{
    private float _currentTime;
    private GameObject[] _redLight;
    private GameObject[] _yellowLight;
    private GameObject[] _greenLight;

    private void Start()
    {
        _redLight = GameObject.FindGameObjectsWithTag("PoleTrafficRed");
        _yellowLight  = GameObject.FindGameObjectsWithTag("PoleTrafficYellow");
        _greenLight = GameObject.FindGameObjectsWithTag("PoleTrafficGreen");
        _currentTime = Time.time;
    }
    

    private void Update()
    {
        var turnRed = Time.time <= _currentTime + 2f;
        var turnGreen = Time.time <= _currentTime + 12f;
        var turnYellow = Time.time <= _currentTime + 4f 
                         || (Time.time > _currentTime + 12f 
                         && Time.time <= _currentTime + 14f);
        
        if (turnRed) ChangeColor("red");
        else if (turnYellow) ChangeColor("yellow");
        else if(turnGreen) ChangeColor("green");
        else _currentTime = Time.time + 12f;
    }

    private void ChangeColor(string color)
    {
        foreach (var light in _redLight)
            light.GetComponent<MeshRenderer>().enabled = color.Equals("red");
        
        foreach (var light in _yellowLight)
            light.GetComponent<MeshRenderer>().enabled = color.Equals("yellow");
        
        foreach (var light in _greenLight)
            light.GetComponent<MeshRenderer>().enabled = color.Equals("green");
    }
}
