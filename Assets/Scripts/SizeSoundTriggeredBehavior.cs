using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeSoundTriggeredBehavior : SoundTriggeredAbstract
{
    public bool followKick = true;
    public float minFactor = 1f;
    public float maxFactor = 2f;

    Transform transform;
    Vector3 initialScale;
   

    public override void Init() {
        transform = gameObject.transform;
        initialScale = transform.localScale;
    }

    public override void Handle(float kickIntensity, float snareIntensity) {
        float intensity = followKick ? kickIntensity : snareIntensity;

        transform.localScale = initialScale * ((maxFactor - minFactor) * intensity + minFactor);
    } 
}
