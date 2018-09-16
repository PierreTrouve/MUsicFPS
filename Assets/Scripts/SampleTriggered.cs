using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTriggered : MonoBehaviour {

    public int index;
    
    Transform transform;
    Vector3 initialScale;
    public bool activated = true;

    // Use this for initialization
    public void Init (int indexParam) {
        index = indexParam;

        transform = gameObject.transform;
        initialScale = transform.localScale;
    }
	
	// Update is called once per frame
	public void Handle (float sampleValue) {
        if (sampleValue > 3)
            sampleValue = 3;
            transform.localScale = initialScale + initialScale * sampleValue;
    }
}
