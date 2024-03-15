using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBGone : MonoBehaviour
{
    [SerializeField] private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Powerup is active");
            Debug.Log("I AM GOING TO FUCKING KILL MYSELF RAAAAAHHHHHHHHHHHH");
            //Destroy(gameObject.);
        }
    }
}
