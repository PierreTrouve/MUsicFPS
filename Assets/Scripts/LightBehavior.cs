using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour {
    private Light light;
    public int channel = 5;


    // Use this for initialization
    void Start () {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        light.intensity = AudioScript.frecencyBands[channel] * 100;
    }


}
