using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamoPotion : Powerups
{
    [SerializeField] private float timer;
    public Image camoImage; 

    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMovement>();
        startHeight = transform.position.y;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        playerMove.SetCamo(true);
        camoImage.gameObject.SetActive(true);
        Debug.Log("camo on");
        StartCoroutine(powerupTimer());

    } 

    private IEnumerator powerupTimer()
    {
       Debug.Log("timer start");
       yield return new WaitForSeconds(timer);
        playerMove.SetCamo(false);
        camoImage.gameObject.SetActive(false);
        Debug.Log("Camo off");
        yield return null;
    }
}
