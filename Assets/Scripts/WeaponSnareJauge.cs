using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSnareJauge : SoundTriggeredAbstract {
    public bool followSnare = true;

    public float minZ = 0.1f;
    public float maxZ = 0.1f;

    protected float initialZ;

    public override void Init() {
        initialZ = transform.localPosition.z;
    }

    public override void Handle(float kickIntensity, float snareIntensity) {
        float intensity = followSnare ? snareIntensity : kickIntensity;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, minZ + intensity * maxZ);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, initialZ + (minZ +intensity * maxZ) / 2f);
    }
}
