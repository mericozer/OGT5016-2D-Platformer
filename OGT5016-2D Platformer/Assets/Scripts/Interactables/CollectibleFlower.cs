using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleFlower : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if player touches to the flower it gets destroyed and flower count increases with canvas controller function
    private void OnTriggerEnter2D(Collider2D other)
    {
        CanvasController.Instance.UpdateCollectibles("Flower");
        Destroy(gameObject);
    }
}
