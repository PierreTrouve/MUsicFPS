using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    // The reduction percent applied to a channel (to smooth the descent)
    public float sampleIntensityReductionPercent = 0.9f;

    // The snare and kick multiplier, to normalize snare intensity to [0,1]
    public float snareMutliplier = 1f;
    public float kickMuliplier = 1f;

    public float kickIntensity, snareIntensity;

    public float[] samples = new float[512];
    protected float[] newSample = new float[512];

    public Instantiate512Cubes bossRingScript;


    AudioSource audioSource;

    // Just for display, to delete
    public GameObject maxPrefab, snarePrefab, kickPrefab;
    int t = 0;

    private List<SoundTriggeredAbstract> subscribers = new List<SoundTriggeredAbstract>();


    public void Init()
    {
        // We get the audio component on which we will listen and analyse the music
        audioSource = GetComponent<AudioSource>();
    }

    public void ComputeIntensities()
    {
        // First we get the whole spectrum. The info will be used for environment animation
        GetSpectrumAudioSource();
        // Then we extract the snare and the kick from the spectrum. The values must be in a range of [0f, 1f]
        GetKickAndSnareIntensity();

        DrawEverything();

        HandleSubscribers();
        bossRingScript.Animate(samples);
    }

    void GetSpectrumAudioSource()
    {
        if (samples == null)
        {
            audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        }
        else
        {
            audioSource.GetSpectrumData(newSample, 0, FFTWindow.Rectangular);
            for (int i = 0; i < 512; i++)
            {
                if (newSample[i] < samples[i] * sampleIntensityReductionPercent)
                {
                    samples[i] = samples[i] * sampleIntensityReductionPercent;
                }
                else
                {
                    samples[i] = newSample[i];
                }
            }
        }
    }

    void GetKickAndSnareIntensity()
    {
        // Get Kick;
        float realKickIntesity = (samples[2] + samples[3] + samples[4]) / 3f * kickMuliplier;
        kickIntensity = realKickIntesity > 1f ? 1f : realKickIntesity;

        // Get Snare
        float realSnareIntensity = 0;
        for (int f = 32; f < 64; f++)
        {
            realSnareIntensity += samples[f] /16f * snareMutliplier; //16f = average *2
        }
        snareIntensity = realSnareIntensity > 1f ? 1f : realSnareIntensity;
    }

    private void DrawEverything()
    {

        if (Input.GetKey("t"))
        {
            /*
            t++;
            GameObject max = Instantiate(maxPrefab);
            max.transform.position = new Vector3(t - 100, 30, 0);

            GameObject kick = Instantiate(kickPrefab);
            kick.transform.position = new Vector3(t - 100, kickIntensity * 30, 0);

            GameObject snare = Instantiate(snarePrefab);
            snare.transform.position = new Vector3(t - 100, snareIntensity * 30, 1);
            */

        }

    }

    public void Subscribe(SoundTriggeredAbstract gameobject)
    {
        subscribers.Add(gameobject);
    }

    void HandleSubscribers()
    {
        foreach (SoundTriggeredAbstract subscriber in subscribers)
        {
            subscriber.Handle(kickIntensity, snareIntensity);
        }
    }
}
