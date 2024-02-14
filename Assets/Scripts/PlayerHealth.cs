using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    
    public float health;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        health = 100f; 
    }

    void GameOver()
    {
        //pause game function
        //make screen pop up with game over, main menu button, and restart level button
        healthText.text = "Game Over";
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + ((int)health).ToString();

        if(health <= 0)
        {
            GameOver();
        }


    }
}
