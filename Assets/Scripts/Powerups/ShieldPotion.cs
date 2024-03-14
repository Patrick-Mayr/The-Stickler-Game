using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPotion : Powerups
{
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMovement>();
        startHeight = transform.position.y; 

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMove.SetShield(true);
        
        StartCoroutine(powerupTimer());
    }

    private IEnumerator powerupTimer()
    {
        
        yield return new WaitForSeconds(duration);
        playerMove.SetShield(false);
        
        yield return null;
    }
}
