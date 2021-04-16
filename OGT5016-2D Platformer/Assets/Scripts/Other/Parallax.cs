using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public GameObject camera;
    
    private float length, startpos;
    
    public float parallaxEffect; //parallax speed for layers
    public float patchHeight; //sprites to cover up sides of the map
    
    public bool isPatch; //checks if any extra sprite
    
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        
        //calculates the movement with sprites width
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //without patch
        //accordign to camera position it calculates whether to slide the background for player or not
        if (!isPatch)
        {
            float temp = (camera.transform.position.x * (1 - parallaxEffect));
            float dist = (camera.transform.position.x * parallaxEffect) - 1f;

            transform.position = new Vector3(startpos + dist, 1.95f, transform.position.z);

            if (temp > startpos + length) startpos += length;
            else if (temp < startpos - (length/20)) startpos -= length;
        }
        //patch moves with original background
        else
        {
            float temp = (camera.transform.position.x * (1 - parallaxEffect));
            float dist = (camera.transform.position.x * parallaxEffect) - 1f;

            transform.position = new Vector3(startpos + dist, patchHeight, transform.position.z);

            if (temp > startpos + length) startpos += length;
            else if (temp < startpos - (length/20)) startpos -= length;
        }

       
    }

   
}
