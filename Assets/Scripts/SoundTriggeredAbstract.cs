using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundTriggeredAbstract : MonoBehaviour
{
    public GameObject musicManager;

    MusicManager musicManagerScript;

    void Start()
    {
        musicManagerScript = musicManager.GetComponent<MusicManager>();
        musicManagerScript.Subscribe(this);
        Init();
    }

    public abstract void Init();

    public abstract void Handle(float kickIntensity, float snareIntensity);
}
