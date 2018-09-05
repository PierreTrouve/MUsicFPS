using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTriggered : MonoBehaviour {

    public int index;
    
    Transform transform;
    Vector3 initialScale;

    // Use this for initialization
    public void Init (int indexParam) {
        index = indexParam;

        transform = gameObject.transform;
        initialScale = transform.localScale;
    }
	
	// Update is called once per frame
	public void Handle (float sampleValue) {
        transform.localScale = initialScale * (40 * sampleValue + 1);
    }
}
