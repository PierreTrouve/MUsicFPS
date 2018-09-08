using UnityEngine;
using System.Collections;

public class Shootable : MonoBehaviour
{

    //The box's current health point total

    public int currentHealth = 100;

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}