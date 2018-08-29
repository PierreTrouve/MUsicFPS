using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSoundTriggeredScript : SoundTriggeredAbstract
{
    Light light;

    public override void Init()
    {
        // We get the audio component on which we will listen and analyse the music
        light = GetComponent<Light>();
    }

    public override void Handle(float kickIntensity, float snareIntensity)
    {
        light.intensity = kickIntensity;
    }
}
