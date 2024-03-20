using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float airSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float jumpHeight;
    [SerializeField] float stamina;
    [SerializeField] float staminaDepletion;
    [SerializeField] float staminaRecovery;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI staminaDisplay;

    Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isSprinting = false;
    private bool hasKnockback = false;
    float staminaAmount;

    bool hasShield;
    bool hasCamo;
    bool hasRepel;
     
    PlayerHealth health;
    [SerializeField] private float knockback;

    //pause menu stuff
    public Image pauseMenu;
    public Lvl1UI lvl1UIScript;
  


    // Start is called before the first frame update
    void Start()
    {
        Manager.Init(this);
        Manager.SetGameControls();
        rb = GetComponent<Rigidbody2D>();
        staminaAmount = stamina;
        staminaDisplay.text = "Stamina: " + staminaAmount;
        health = gameObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            hasKnockback = false;
        }

        if (!isSprinting && IsGrounded() && !hasKnockback)
        {
            Vector2 targetVelocity = new Vector2(moveDirection.x, 0.0f) * speed;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetVelocity.x, acceleration * Time.deltaTime), rb.velocity.y);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speed, speed), rb.velocity.y);
        }
        else if (isSprinting && IsGrounded() && !hasKnockback)
        {
            Vector2 targetVelocity = new Vector2(moveDirection.x, 0.0f) * sprintSpeed;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetVelocity.x, acceleration * Time.deltaTime), rb.velocity.y);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -sprintSpeed, sprintSpeed), rb.velocity.y);
        } else if (!isSprinting && !IsGrounded() && !hasKnockback)
        {
            Vector2 airMove = new Vector2(moveDirection.x, 0) * airSpeed;
            rb.AddForce(airMove);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speed, speed), rb.velocity.y);
        }
        else if (isSprinting && !IsGrounded() && !hasKnockback)
        {
            Vector2 airMove = new Vector2(moveDirection.x, 0) * airSpeed;
            rb.AddForce(airMove);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -sprintSpeed, sprintSpeed), rb.velocity.y);
        } 
        
    }

    public void SetMovementDirection(Vector2 currentDirection)
    {
        moveDirection = currentDirection;
        if (currentDirection.x > 0)
        {
            transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        } else if (currentDirection.x < 0)
        {
            transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);
        }
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
        if (IsGrounded())
        {
            isSprinting = true;
            StartCoroutine(StaminaBar());
        }
    }
    public void StopSprint()
    {

        isSprinting = false;
    }

    public void PauseGame()
    {


        if (lvl1UIScript.gameOver == false)
        {
            //pause game time
            Time.timeScale = 0;

            //make pause menu pop up
            pauseMenu.gameObject.SetActive(true);
        }
 
    }

    public IEnumerator StaminaBar()
    {

        while (isSprinting && staminaAmount > 0f)
        {
            yield return new WaitForSeconds(staminaDepletion);
            staminaAmount--;
            //staminaAmount = Mathf.Clamp(staminaAmount, 0f, stamina);
            //Debug.Log("Deplete: " + staminaAmount.ToString());
            staminaDisplay.text = "Stamina: " + staminaAmount;
        }

        if (staminaAmount == 0f)
        {
            isSprinting = false;
        }
        while (!isSprinting && staminaAmount < stamina)
        {
            yield return new WaitForSeconds(staminaRecovery);
            staminaAmount++;
            //staminaAmount = Mathf.Clamp(staminaAmount, 0f, stamina);
            //Debug.Log("Recovery: " + staminaAmount.ToString());
            staminaDisplay.text = "Stamina: " + staminaAmount;
        }
        yield return null;
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.8f, groundLayer);

        if (hit.collider != null)
        {
            //Debug.Log("Grounded");
            return true;
        }
        else
        {
            //Debug.Log("Not Grounded");
            return false;
        }
    }

    public bool GetShield()
    {
        return hasShield;
    }
    public void SetShield(bool shield)
    {
        
        if (shield == true)
        {
            Debug.Log("shield on");
            hasShield = shield;
        } 
        else if (shield == false)
        {
            Debug.Log("coroutine");
            StartCoroutine(shieldTimer());
        }
    }

    public bool GetCamo()
    {
        return hasCamo;
    }
    public void SetCamo(bool camo)
    {
        Debug.Log("set camo");
        hasCamo = camo;
    }

    public bool GetRepel()
    {
        return hasRepel;
    }
    public void SetRepel(bool repel)
    { 
        hasRepel = repel;
    }

    private IEnumerator shieldTimer()
    {
        
        yield return new WaitForSeconds(2);
        hasShield = false;
        Debug.Log("shield off");
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("levelend"))
        {
            lvl1UIScript.gameOver = true;
            lvl1UIScript.levelCompleted = true;
        }
    } 

    public void Knockback(Collision2D collision)
    {
        hasKnockback = true;
        if (IsGrounded())
        {
            Vector2 knockbackAmount = new Vector2(-collision.contacts[0].normal.x * knockback, 0.1f * knockback);
            
            rb.AddForce(knockbackAmount, ForceMode2D.Impulse);
        }
        else if(!IsGrounded())
        {
            if (collision.contacts[0].normal.y < 0) 
            {
                Vector2 knockbackAmount = new Vector2( -knockback * 0.075f, knockback * 0.1f);
                
                rb.AddForce(knockbackAmount, ForceMode2D.Impulse);
            }
            
        } 
        

    }
    
}
