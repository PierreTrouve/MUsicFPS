using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]

public class AudioScript : MonoBehaviour {
    AudioSource audioSource;

    public GameObject prefabs;
    public static float[] samples = new float[512];
    protected float[] previousSample = new float[512];
    protected float[] newSample = new float[512];
    public static float[] frecencyBands = new float[8];

    public int channel = 0;


    protected int t = 0;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        GetSpectrumAudioSource();
        makeFrequencyBands();
        DrawEverything();
    }

    void GetSpectrumAudioSource()
    {
        if (samples == null)
        {
            audioSource.GetSpectrumData(samples, channel, FFTWindow.Blackman);
        } else
        {
            audioSource.GetSpectrumData(newSample, channel, FFTWindow.Rectangular);
            for (int i = 0; i < 512; i++)
            {
                if (newSample[i] < samples[i] *0.9f)
                {
                    samples[i] = samples[i] * 0.9f;
                } else
                {
                    samples[i] = newSample[i];
                }
            }
        }
    }

    private void DrawEverything()
    {
        
        if (Input.GetKey("e"))
        {
            t++;
            for (int i = 0; i < 100; i++)
            {
                GameObject cube = Instantiate(prefabs);
                cube.transform.position = new Vector3(t - 100, samples[i] * 30, i);
            }
        }
    
    }

    void makeFrequencyBands()
    {
        /*
         * 22050 / 512  = 43 hertz by sample
         * 20
         * 60
         * 250
         * 500
         * 2000
         * 4000
         * 6000 -> 20000
         * 
         * 0-2 => 86
         * 
         * 
         */

        int count = 0;

        for (int i = 0; i<8; i++)
        {
            float avg = 0;
            int sampleCount = (int)Mathf.Pow(2, i);

            for (int j = sampleCount; j < sampleCount * 2; j++)
            {
                avg += samples[count++];
            }

            frecencyBands[i] = avg / sampleCount * i;
        }
    }
}
