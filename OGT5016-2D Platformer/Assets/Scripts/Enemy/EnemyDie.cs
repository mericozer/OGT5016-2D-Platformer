using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Enemy script for enemies heads a.k.a. Kill Spot
public class EnemyDie : MonoBehaviour
{
    //Because kill spot works different than enemy behaviour it needs its own script
    //But also it works for enemy so some functions are needed from enemy behaviour
    //Events are in use to use enemy behaviour functiions
    [Header("Events")]
    [Space]

    public UnityEvent OnDieEvent;

    private Collider2D collider;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        if (OnDieEvent == null)
        {
            OnDieEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        //disables kill spot collider
        //destroys enemy object after 2 seconds
        if (other.collider.CompareTag("Hero"))
        {
            OnDieEvent.Invoke();
            Destroy(collider);
            Destroy(transform.parent.gameObject, 2f);
        }
    }
}
