using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Collider2D collider;  
    
    private Rigidbody2D rgb;
    
    private bool doubleJump; //checks if can hero do a double jump
    private bool isFinished = false; //checks if level finished or not
    private bool isDead = false; //checks if character is dead or not
    
    private float horizontalMove; //takes input values for right and left moves

    [SerializeField] private float runSpeed = 40f;
    [SerializeField] private float jumpSpeed = 120f;
    
    [SerializeField]
    private LayerMask groundLayerMask; //ground check
    [SerializeField]
    private LayerMask groundExtraLayerMask; //extra ground check mask for player character only
    [SerializeField]
    private LayerMask SpikeMask; //checks for spike layers

    private Vector3 m_Velocity = Vector3.zero; 
    
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent <Collider2D>();
        rgb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isFinished && !isDead) //if level ends, player starts to run without input 
        {

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            //due to movement rotation if statement flips players rotation
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0 , 0, 0);
            }
            else if(Input.GetAxisRaw("Horizontal") < 0)
            {
                transform.rotation = Quaternion.Euler(0 , 180, 0);
            }

            _animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            
            //ground check
            if (IsGround())
            {
                doubleJump = true;
                _animator.SetBool("IsJumping", false);
            }

            //jump input
            if (Input.GetButtonDown("Jump"))
            {
                _animator.SetBool("IsJumping", true);
                if (IsGround())
                {
                    rgb.AddForce(new Vector2(0f, jumpSpeed));
                }
                else
                {
                    if (doubleJump)  //checks double jump
                    {
                        rgb.AddForce(new Vector2(0f, jumpSpeed));
                        doubleJump = false;
                    }
                }
            }
        }
        else  //run without input
        {
            horizontalMove = 1.5f * runSpeed;
            _animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        
      
    }

    private void FixedUpdate()
    {
        //apply input as force with some velocity 
        if (!isDead)
        {
            Vector3 targetVelocity = new Vector2(horizontalMove, rgb.velocity.y);
            // smoothing it out and applying it to the character
            rgb.velocity = Vector3.SmoothDamp(rgb.velocity, targetVelocity, ref m_Velocity, 0.05f);
        }
        
    }

    private bool IsGround()
    {
        float offset = 0.01f;

        bool returnValue;
        
        //sending ray to check ground layer
        RaycastHit2D rayCastHit= Physics2D.Raycast(collider.bounds.center, 
            Vector2.down, 
            collider.bounds.extents.y + offset,
            groundLayerMask);

        //sending ray to check extra ground layer, which is only for player character
        RaycastHit2D rayCastHitExtra= Physics2D.Raycast(collider.bounds.center, 
            Vector2.down, 
            collider.bounds.extents.y + offset,
            groundExtraLayerMask);

        //checks if at least one of the ground layer received the ray
        if (rayCastHit.collider != null)
        {
            returnValue = true;
        }
        else if(rayCastHitExtra.collider != null)
        {
            returnValue = true;
        }
        else
        {
            returnValue = false;
        }
        
        //visual check for debug
        Color rayColor;
        if (rayCastHit.collider != null)
        {
            rayColor = Color.blue;
        }
        else
        {
            rayColor = Color.green;
        }
        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + offset), rayColor);

        return returnValue;
    }


    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.collider.CompareTag("Spike") || other.collider.CompareTag("Water"))
        {
          CanvasController.Instance.UpdateLifeStats();
          DeadProtocol();
        }
        //enemies can die with bumping to the heads, "Head" is tag for enemies Kill Spot
        else if (other.collider.CompareTag("Head"))
        {
            rgb.velocity = Vector3.zero;
            rgb.angularVelocity = 0;
            rgb.AddForce(new Vector2(0f, jumpSpeed));
        }
        
        //Enemy itself harms the player
        else if (other.collider.CompareTag("Enemy"))
        {
            DeadProtocol();
            CanvasController.Instance.UpdateLifeStats();
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        //checks if level is finished or not
        if (other.CompareTag("Finish"))
        {
            isFinished = true;
            StartCoroutine(CanvasController.Instance.OpenPanel("Win"));
        }
    }
    

    //disables players collider, make player jump once without velocity and plays die animation
    private void DeadProtocol()
    {
        isDead = true;

        _animator.SetBool("IsDead", isDead);
            
        rgb.velocity = Vector3.zero;
        rgb.angularVelocity = 0;
        rgb.AddForce(new Vector2(0f, jumpSpeed));
            
        StartCoroutine(CanvasController.Instance.OpenPanel("Dead"));
            
        collider.enabled = false;
    }
}
