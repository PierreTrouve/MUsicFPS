using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour {

    int currentPosition = 0;
    public Material marvelousMaterial;
    public Material perfectLateMaterial;
    public Material perfectEarlysMaterial;
    public Material goodLateMaterial;
    public Material goodEarlyMaterial;
    public Material missLateMaterial;
    public Material missEarlyMaterial;

    public int GetPositionAndUpdate() {
        currentPosition++;
        return currentPosition;
    }

    private void Update() {
        if (Input.GetKeyDown("t")) {
            GetDamageFactor();
        }
    }

    public int GetDelta() {
        return 50 - currentPosition;
    }

    public int GetDamageFactor() {
        int delta = GetDelta();
        Renderer renderer = gameObject.GetComponent<Renderer>();

        // Marvelous
        if (delta == 0) {
            renderer.material = marvelousMaterial;
            return 3;
        }

        //Perfect
        if (delta > 0 && delta <= 5) {
            renderer.material = perfectEarlysMaterial;
            return 2;
        }
        if (delta < 0 && delta >= -5) {
            renderer.material = perfectLateMaterial;
            return 2;
        }

        //Good
        if (delta > 5 && delta <= 25) {
            renderer.material = goodEarlyMaterial;
            return 1;
        }
        if (delta < -5 && delta >= -25) {
            renderer.material = goodLateMaterial;
            return 1;
        }

        //Miss
        if (delta > 25) {
            renderer.material = missEarlyMaterial;
            return 0;
        }
        if (delta < -25 ){
            renderer.material = missLateMaterial;
            return 0;
        }


        return 0;
    }
}
