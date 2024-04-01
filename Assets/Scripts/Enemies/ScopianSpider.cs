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
    private PlayerHealth playerHealthScript;

    private PlayerMovement playerMovement;
    
    private GameObject player;
    bool facingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pointA.transform.position;
        //rb = GetComponent<Rigidbody2D>();
        //currentPosition = pointB.transform;
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealthScript = player.GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player") && !playerMovement.GetShield())
        {
            playerHealthScript.DealDamage(10f);
            playerMovement.Knockback(collision);
        }
        else if (collision.gameObject.tag == ("Player") && playerMovement.GetShield())
        {
            playerMovement.SetShield(false);
        }
    }

    public void ChangeFacingDirection()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

        
    }

    // Update is called once per frame
    void Update()
    {


        if (Vector2.Distance(transform.position, pointB.transform.position) <= 0.1f)
        {
            moveToA = true;
            if (!facingLeft)
            {
                facingLeft = true;
                ChangeFacingDirection();
            }

        }
        else if(Vector2.Distance(transform.position, pointA.transform.position) <= 0.1f)
        {
            moveToA = false;
            if (facingLeft)
            {
                facingLeft = false;
                ChangeFacingDirection();
            }
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
