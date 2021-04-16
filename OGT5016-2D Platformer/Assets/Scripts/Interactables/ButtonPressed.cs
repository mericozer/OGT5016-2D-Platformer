using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//There are two kinds of buttons

//Green Button
//When activated, the objects that connected to button starts to work without stopping

//Red Button
//When activated , the object that connected to button makes its move one time for one press

//Buttons can be twin buttons which mean if one is pressed the other one will be pressed too.

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] private GameObject pressedState; //Sprite for pressed button

    [SerializeField] private float activateTime = 15f; //Red button activate time

    [SerializeField] bool isButtonGreen; //checks the button color
    
    private bool firstPress = true;
    
    private SpriteRenderer spriteRenderer;
    private Collider2D coll;
    
    //Events are in use for interactive objects
    public UnityEvent OnPressEvent;

    
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        if (OnPressEvent == null)
        {
            OnPressEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        
        if (other.collider.CompareTag("Hero"))
        {
            //if button read is starts coroutine to make button active again
            if (!isButtonGreen)
            {
                StartCoroutine(ActivateButton());
                coll.enabled = false;
                spriteRenderer.enabled = false;
            }
            //if button green it destroys the active button sprite
            else
            {
                Destroy(gameObject);
            }
            
            Press();
            
            OnPressEvent.Invoke();
            pressedState.SetActive(false);
        }
        
    }

    //makes button switch to pressed sprite
    //if it is the first time is creates the pressed sprite in the scene
    private void Press()
    {
        if (firstPress)
        {
            pressedState.SetActive(true);
            Instantiate(pressedState, gameObject.transform.position, transform.rotation);
                
            firstPress = false;
        }
        else
        {
            pressedState.SetActive(true);
        }
    }

    //makes the twin button to pass to pressed state
    public void TwinButton()
    {
        Press();
        StartCoroutine(ActivateButton());
        coll.enabled = false;
        spriteRenderer.enabled = false;
        pressedState.SetActive(false);
    }
    
    private void OnDestroy()
    {
        pressedState.SetActive(true);
    }

    //makes button unpressed after a time
    private IEnumerator ActivateButton()
    {
        yield return new WaitForSeconds(activateTime);
        
        coll.enabled = true;
        spriteRenderer.enabled = true;
        
    }

    
}
