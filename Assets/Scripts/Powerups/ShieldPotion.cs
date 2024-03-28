using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldPotion : Powerups
{
    public Image shieldImage;

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMovement>();
        startHeight = transform.position.y; 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMove.SetShield(true);

        shieldImage.gameObject.SetActive(true);
        
        StartCoroutine(powerupTimer());
    }

    private IEnumerator powerupTimer()
    {
        
        yield return new WaitForSeconds(duration);
        playerMove.SetShield(false);

        shieldImage.gameObject.SetActive(false);
        
        yield return null;
    }
}
