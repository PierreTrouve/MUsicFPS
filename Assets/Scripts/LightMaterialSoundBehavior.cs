using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMaterialSoundBehavior : SoundTriggeredAbstract
{
    Material material;
    

    public override void InitChild() {
        material = GetComponent<Material>();
    }

    public override void Handle(float kickIntensity, float snareIntensity) {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        float emission = kickIntensity;
        Color baseColor = new Color(kickIntensity, snareIntensity, 1,1) ; //Replace this with whatever you want for your base color at emission level '1'

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        Debug.Log("final color");
        mat.SetColor("_EmissionColor", finalColor);
    }
}
