using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public int maxHealth = 100;
    public int health = 100;

    public Texture2D hpBarTexture;
    public float maxBarLenght = 200;
    public float hpBarLenght;

    private void Start() {
        hpBarLenght = maxBarLenght;
    }

    public void ReceiveDamage(int dmg) {
        health -= dmg;
        hpBarLenght = maxBarLenght * health / maxHealth;
        if (health < 0 ) {
            Debug.Log("Dead !");
        }
    }

    private void OnGUI() {
        if (health > 0) {
            GUI.DrawTexture(
                new Rect(10, 10, hpBarLenght, 10),
                hpBarTexture
            );
        }
    }
}
