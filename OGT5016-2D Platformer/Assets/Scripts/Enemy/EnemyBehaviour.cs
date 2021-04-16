using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class EnemyBehaviour : MonoBehaviour
{
    private bool toLeft = false;
    private bool isDead = false;
    
    private float moveSpeed = 5f;
    
    private Animator _animator;
    
    private Collider2D coll;

    private Rigidbody2D rgb;

    [SerializeField] private bool walkOnGround; //checks if enemy is a walking enemy or flying enemy. Ray check changes over this info
    
    [SerializeField] private float heightOffset = 3f; //ray check offset
    
    [SerializeField] private LayerMask groundLayerMask;
    
    [SerializeField] private Transform groundChecker; 
    
   
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rgb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement is only available when enemy is alive
        if (!isDead)
        {
            //enemies always starts to moving left
            //due to ray check their rotaiton flips from left to right or vice versa
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (IsGround() == !walkOnGround)
            {
                if (toLeft)
                {
                    transform.rotation = Quaternion.Euler(0 , 0, 0);
                    toLeft = false;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0 , 180, 0);
                    toLeft = true;
                }
            }

           
        }
        
    }
    

    //Ground check with raycasting
    private bool IsGround()
    {
       
        RaycastHit2D rayCastHit= Physics2D.Raycast(groundChecker.position, 
            Vector2.down, 
            heightOffset,
            groundLayerMask);

        //visual check for debuging
        Color rayColor;
        if (rayCastHit.collider != null)
        {
            rayColor = Color.blue;
        }
        else
        {
            rayColor = Color.green;
        }
        Debug.DrawRay(groundChecker.position, Vector2.down *  + heightOffset, rayColor);
        

        return rayCastHit.collider != null;
    }
    

    //disables colliders for the enemy and plays dead animation
    public void Dead()
    {
        isDead = true;
        coll.enabled = false;
        rgb.gravityScale = 2f;
        _animator.SetBool("IsDead", true);
    }
    
}
