using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopianSpider : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPosition;
    public float speed;
    bool moveToA;
    public PlayerHealth playerHealthScript; 

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pointA.transform.position;
        //rb = GetComponent<Rigidbody2D>();
        //currentPosition = pointB.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            playerHealthScript.health -= 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Vector2.Distance(transform.position, pointB.transform.position) <= 0.1f)
        {
            moveToA = true; 
            
        }
        else if(Vector2.Distance(transform.position, pointA.transform.position) <= 0.1f)
        {
            moveToA = false;
            
        }

        if (moveToA == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.transform.position, speed * Time.deltaTime);
        }
        else if(moveToA == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.transform.position, speed * Time.deltaTime);
        }

        
    }
}