using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnController : MonoBehaviour
{
    //Takes all the columns and make thier button pressed function work
    //it should be connected to a button or a lock
    
    public List<GameObject> columns = new List<GameObject>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void ButtonPressed()
    {
        foreach (var col in columns)
        {
            col.GetComponent<MoveUpDown>().ButtonPressed();
        }
    }
}