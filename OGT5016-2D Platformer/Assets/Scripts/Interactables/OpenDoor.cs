using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    //Door is for the end of the level
    //It includes three part for three flowers
    
    [SerializeField] private int collectibleFlower; //checks if the collected flower count is enough (it needs to be three)

    [SerializeField] private OpenDoor extraDoor; //if level includes more than three flowers it increass as coefficents of 3 
                                                 // extra door is need to work with the main door

    [SerializeField] private bool extra; //checks if there is an extra door
    
    private Animator _animator; 

    private Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
      
        if ( CanvasController.Instance.FlowerCount == collectibleFlower && other.collider.CompareTag("Hero"))
        {
            //if there is an extra door it plays the extra doors opening animation
            if(extra)
            {ExtraDoor();}
            Open();
           
           
        }
    }

    //plays opening animation
    private void Open()
    {
        _animator.SetBool("Open", true);
        StartCoroutine(Delay());
    }

    //opening animation for extra door
    public void ExtraDoor()
    {
        extraDoor.Open();
    }
    

    //while door is opening collider must wait for the animation ends
    //it is approximately 2 seconds
    IEnumerator Delay(){
       
        yield return new WaitForSeconds(2f);
        coll.enabled = false;
        
    }
    
    
}
