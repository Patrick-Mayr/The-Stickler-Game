using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float sprintSpeed; 
    [SerializeField] float acceleration;
    [SerializeField] float jumpHeight;
    [SerializeField] private LayerMask groundLayer;
    
    Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isSprinting = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Manager.Init(this);
        Manager.SetGameControls();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSprinting)
        {
            Vector2 targetVelocity = new Vector2(moveDirection.x, 0.0f) * speed;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetVelocity.x, acceleration * Time.deltaTime), rb.velocity.y);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speed, speed), rb.velocity.y);
        }
        else
        {
            Vector2 targetVelocity = new Vector2(moveDirection.x, 0.0f) * sprintSpeed;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetVelocity.x, acceleration * Time.deltaTime), rb.velocity.y);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -sprintSpeed, sprintSpeed), rb.velocity.y);
        }
       
    }

    public void SetMovementDirection(Vector2 currentDirection)
    {
        moveDirection = currentDirection;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce((Vector2.up * jumpHeight), ForceMode2D.Impulse);
        }
        
    }

    public void Sprint()
    {
        
        isSprinting = true;
    }
    public void StopSprint()
    {
        
        isSprinting = false;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.8f, groundLayer);
        
        if (hit.collider != null)
        {
            Debug.Log("Grounded");
            return true;
        }
        else
        {
            Debug.Log("Not Grounded");
            return false;
        }
    }
}
