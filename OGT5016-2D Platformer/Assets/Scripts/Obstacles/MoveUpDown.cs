using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{

    private bool canMove = false; 
    
    private float timer;
    [SerializeField]
    private float heightVarience = 10f;
    [SerializeField]
    private float moveSpeed;
    
    private int seconds;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    private void FixedUpdate()
    {
        Timer();
        //make columns move up and down with the value of positive and negative values of the Sinus
        if (canMove)
        {
            transform.position += Vector3.up * Mathf.Sin(timer * moveSpeed) * Time.fixedDeltaTime * heightVarience;
        }
       


    }

    public void ButtonPressed()
    {
        canMove = true;
        ResetTimer();
    }
    
    //columns move with time variable
    //so the time should be zero when they start to move
    private void Timer()
    {
        // seconds
        timer += Time.deltaTime;
        // turn float seonds to int
        seconds = (int)(timer % 60);
        
    }
 
    //makes time value zero
    private void ResetTimer()
    {
        // seconds
        timer += Time.deltaTime;
        // turn float seonds to int
        seconds = (int)(timer % 60);
        
        timer = 0.0f;
      
    }
}
