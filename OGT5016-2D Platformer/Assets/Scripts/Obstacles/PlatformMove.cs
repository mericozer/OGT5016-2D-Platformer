using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//makes platform moves between two points

//should be connected to a button
public class PlatformMove : MonoBehaviour
{
    private bool isPressed = false;
    private bool onPosA = true;
    private bool onWait = false;
    
    [SerializeField] private bool isButtonGreen; //chekcs the button color

    [SerializeField] private float platformSpeed;

    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    
    private float timer;

    private int seconds;
    
    private Collider2D coll;
    
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        if (isPressed)
        {
            //if button is green moves without stoping
            if (isButtonGreen)
            {
                transform.position = Vector3.Lerp (start.transform.position, end.transform.position, Mathf.PingPong(timer * platformSpeed, 1.0f));
            }
            else
            {
                SwitchPositions();
            }
            
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Hero"))
        {
            other.gameObject.transform.parent = gameObject.transform;
        }
        
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
    
    //if button is red
    //it moves from one point to another
    //waits for a time 
    //moves to starting points
    void SwitchPositions ()
    {
            //from start to end
            if (onPosA == true)
            {
                transform.position = Vector3.Lerp (start.transform.position, end.transform.position, Mathf.PingPong(timer * platformSpeed, 1.1f));
                if(transform.position == end.transform.position)
                {
                    //onPosA = false;
                    onWait = true;
                    onPosA = false;

                }
            }
            //wait
            if (onWait)
            {
                StartCoroutine(Delay());
            }
            //from end to start
            else if (onPosA == false)
            {
                transform.position = Vector3.Lerp (end.transform.position, start.transform.position, Mathf.PingPong(timer * platformSpeed, 1.1f));
                if(transform.position == start.transform.position)
                {
                    isPressed = false;
                    onPosA = true;
                }
            }
        
    }
    
    //makes platform move
    public void ButtonPressed()
    {
        isPressed = true;
        ResetTimer();
    }
    
    //platforms move with time variable
    //so the time should be zero when they start to move
    private void Timer()
    {
        // seconds
        timer += Time.deltaTime;
        // turn float seonds to int
        seconds = (int)(timer % 60);
        
    }
 
    //rests the time
    private void ResetTimer()
    {
        // seconds
        timer += Time.deltaTime;
        // turn float seonds to int
        //seconds = (int)(timer % 60);
        
        timer = 0.0f;
      
    }

    //makes platform wait for a time
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.7f);
        onWait = false;
        
        ResetTimer();
    }

}
