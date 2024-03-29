using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    public float health;
    public TextMeshProUGUI healthText;
    
    public Slider slider; 

    // Start is called before the first frame update
    void Start()
    {
        health = 100f; 
    }

    float ReturnPlayerHealth()
    {
        return health;
    }

    void GameOver()
    {
        
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

        slider.value = health / 100f;
    } 

    public void DealDamage(float damage)
    {
        health -= damage;
    }
}
