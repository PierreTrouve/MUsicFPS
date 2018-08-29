using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour {

    public GameObject sampleCubePrefab;
    public float height = 50f;
    public float radius = 0.05f;
    public float cubeSize = 0.005f;

    GameObject[] samplesCubes = new GameObject[64];

	// Use this for initialization
	void Start () {
		
        for (int a = 0; a < 64; a++)
        {
            GameObject sampleCubeInstance = (GameObject)Instantiate(sampleCubePrefab);
            sampleCubeInstance.transform.position = this.transform.position;
            sampleCubePrefab.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
            sampleCubeInstance.transform.parent = this.transform;
            sampleCubeInstance.name = "SampleCube" + a;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * 8 * a, 0);
            sampleCubeInstance.transform.position = new Vector3((float)0f, (float)this.transform.position.y, (float)radius);
            samplesCubes[a] = sampleCubeInstance;
        }
	}

    // Update is called once per frame
    public void Animate(float[] samples)
    {
        for (int a = 0; a < 64; a++)
        {
            int b = a;
            if (b > 32)
            {
                b = 64 - a;
            }
            samplesCubes[a].transform.localScale = cubeSize * new Vector3(1, MaxedNumber(0.1f,samples[b]) * height, 1);
            
        }
    }

    protected float MaxedNumber(float max, float value) {
        return value > max ? max : value;
    }
}
