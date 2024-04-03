using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
//using UnityEditor.Build;

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

   [SerializeField] private Animator player = new Animator();

    bool hasShield;
    bool hasCamo;
    bool hasRepel;
     
    PlayerHealth health;
    [SerializeField] private float knockback;

    //pause menu stuff
    public Image pauseMenu;
    public Lvl1UI lvl1UIScript;

    //HUD stuff
    public Slider staminaBar;
    public Image shieldImage;

    private bool idle;
    
    private bool run;
    private bool jump;
    private bool damage;


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
            
            

            jump = false;
            player.SetBool("Jump", jump); 

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

        staminaBar.value = staminaAmount / 100f;
    }

    private void LateUpdate()
    {
        if(IsGrounded() && hasKnockback)
        {
            hasKnockback = false;
            damage = false;
            player.SetBool("Damage", damage);

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

        if (currentDirection.x != 0)
        {
            run = true;
            player.SetBool("Run", run);

        }
        else
        {
            run = false;
            player.SetBool("Run", run);

        }
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            jump = true;
            player.SetBool("Jump", jump);

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
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * hit.distance, Color.red);

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

        shieldImage.gameObject.SetActive(false);
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("levelend"))
        {
            lvl1UIScript.gameOver = true;
            lvl1UIScript.levelCompleted = true;
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            lvl1UIScript.gameOver = true;
            Debug.Log("You hit spikes");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            lvl1UIScript.gameOver = true;
            Debug.Log("You fell out of the map");
        }
    }

    public void Knockback(Collision2D collision)
    {
        hasKnockback = true;

        damage = true;
        player.SetBool("Damage", damage);

        if (IsGrounded())
        {
            Vector2 knockbackAmount = new Vector2(-collision.contacts[0].normal.x * knockback, 0.2f * knockback);
            rb.AddForce(knockbackAmount, ForceMode2D.Impulse);

            //StartCoroutine(KnockbackOnGround());
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

    public IEnumerator KnockbackOnGround()
    {
        yield return new WaitForSeconds(0.1f);

        hasKnockback = true;

        damage = true;
        player.SetBool("Damage", damage);

        yield return null;
    }
    
}
