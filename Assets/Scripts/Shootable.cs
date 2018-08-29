using UnityEngine;
using System.Collections;

public class Shootable : MonoBehaviour
{

    //The box's current health point total

    public int currentHealth = 100;
    public GameObject player;

    public TextMesh healthtextMesh;

    public void Damage(int damageAmount)

    {

        //subtract damage amount when Damage function is called

        currentHealth -= damageAmount;

        healthtextMesh.text =  currentHealth.ToString();

        //Check if health has fallen below zero

        if (currentHealth <= 0)

        {

            //if health has fallen below zero, deactivate it 

            gameObject.SetActive(false);

        }

    }

    void Update()
    {
        
    }

}