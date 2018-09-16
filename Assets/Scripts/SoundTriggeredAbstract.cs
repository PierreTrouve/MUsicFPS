using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SoundTriggeredAbstract : MonoBehaviour
{
    MusicManager musicManagerScript;

    void Start() {
        MusicManager[] musicManagers = FindObjectsOfType(typeof(MusicManager)) as MusicManager[];
        musicManagerScript = musicManagers[0];
        musicManagerScript.Subscribe(this);
        InitChild();
    }

    public void Init()
    {
        MusicManager[] musicManagers = FindObjectsOfType(typeof(MusicManager)) as MusicManager[];
        musicManagerScript = musicManagers[0];
        musicManagerScript.Subscribe(this);
        InitChild();
    }

    public abstract void InitChild();

    public abstract void Handle(float kickIntensity, float snareIntensity);
}
