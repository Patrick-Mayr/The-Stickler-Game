using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    private GameObject player;
    
    public float speed;
    float angle;
    private float distance;
    public float attentionRadius = 10;
    public GameObject pointA;
    public GameObject pointB;
    bool goToA = true;
    float angleA;
    float angleB;
    
    private PlayerHealth playerHealthScript;
    private PlayerMovement playerMovement;

    bool facingLeft = false; 

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.position = pointB.transform.position;
        
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
            playerMovement.Knockback(collision);
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
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        //angle between player and enemy
        angle = Mathf.Atan2(-direction.y, direction.x) * Mathf.Rad2Deg;


        Vector2 directionA = pointA.transform.position - transform.position;
        directionA.Normalize();
        //angle between enemy and point A
        angleA = Mathf.Atan2(-directionA.y, directionA.x) * Mathf.Rad2Deg;

        Vector2 directionB = pointB.transform.position - transform.position;
        directionB.Normalize();
        //angle between enemy and point B
        angleB = Mathf.Atan2(directionB.y, directionB.x) * Mathf.Rad2Deg;



        if (distance <= attentionRadius && !playerMovement.GetCamo())
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            //enemy turns towards player
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            //put what enemy would do if not near player (move up and down)
            if (goToA == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, pointA.transform.position, speed * Time.deltaTime);
                //transform.rotation = Quaternion.Euler(Vector3.forward * angleA);
                if (Vector2.Distance(transform.position, pointA.transform.position) <= 0.5f)
                {
                    goToA = false;
                }

                if (!facingLeft)
                {
                    facingLeft = true;
                    ChangeFacingDirection();
                }
            }
            else if (goToA == false)
            {
                //transform.rotation = Quaternion.Euler(Vector3.forward * angleB);
                transform.position = Vector2.MoveTowards(transform.position, pointB.transform.position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, pointB.transform.position) <= 0.5f)
                {
                    goToA = true;
                }

                if (facingLeft)
                {
                    facingLeft = false;
                    ChangeFacingDirection();
                }
            }

        }
    }
}
