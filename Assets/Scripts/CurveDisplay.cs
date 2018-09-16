using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveDisplay : MonoBehaviour {

    LineRenderer lineRenderer;
    public int count = 0;
	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log((float)count / 100f);
        float value = Mathf.Cos((float)count / 100f);
        lineRenderer.SetPosition(count, new Vector3(0, value, (float)count / 100));
        count++;
	}
}
