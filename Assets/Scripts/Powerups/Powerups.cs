using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public float duration;
    public float x;
    public float y;
    public float startHeight;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
    }

     void Update()
    {
        x += 0.001f * Mathf.PI;
        y = 0.5f * Mathf.Sin(x) + startHeight;

        transform.position = (new Vector2(transform.position.x, y));
    }
}
