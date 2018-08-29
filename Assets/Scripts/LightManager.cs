using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

    public List<GameObject> lamps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("a"))
        {
            switchOn();
        }

        if (Input.GetKeyDown("z"))
        {
            switchOff();
        }
    }

    public void subscribeLamp(GameObject lamp)
    {
        lamps.Add(lamp);
    }

    public void switchOn()
    {
        foreach (GameObject lamp in lamps)
        {
            Light light = lamp.GetComponent<Light>();
            light.intensity = 1;
            Color lightColor = light.color;
            lightColor.b = Mathf.Min(1, Mathf.Max(0, lightColor.b + Random.Range(-0.1f, 0.1f)));
            lightColor.g = Mathf.Min(1, Mathf.Max(0, lightColor.g + Random.Range(-0.1f, 0.1f)));
            lightColor.r = Mathf.Min(1, Mathf.Max(0, lightColor.r + Random.Range(-0.1f, 0.1f)));
            light.color = lightColor;

        }
    }

    public void switchOff()
    {
        foreach (GameObject lamp in lamps)
        {
            Light light = lamp.GetComponent<Light>();
            light.intensity = 0;
        }
    }
}
