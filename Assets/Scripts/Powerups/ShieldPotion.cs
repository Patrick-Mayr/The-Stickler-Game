using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPotion : Powerups
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("inheritance");
        startHeight = transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered from child");
    }
}
