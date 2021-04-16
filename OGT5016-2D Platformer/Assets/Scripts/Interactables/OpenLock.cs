using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Locks are working with keys
//Every lock works with some object
//For make these objects work Events are in use
public class OpenLock : MonoBehaviour
{
    [SerializeField] private GameObject lockOpened;
    [SerializeField] private GameObject lockClosed;
    // Start is called before the first frame update
    
    public UnityEvent OnOpenEvent;

    
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    
    void Start()
    {
        if (OnOpenEvent == null)
        {
            OnOpenEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if player has a key, invokes the event and changes the spirite to pen lock
        if (CanvasController.Instance.HaveKey())
        {
            Instantiate(lockOpened, lockClosed.transform.position, lockClosed.transform.rotation);
            Destroy(gameObject);
            OnOpenEvent.Invoke();
           
        }

       
    }
}
