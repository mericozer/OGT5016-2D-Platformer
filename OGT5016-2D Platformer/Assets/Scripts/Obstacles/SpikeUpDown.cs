using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//eventhough scripts name is up down it only make spikes move between to points on the scene
public class SpikeUpDown : MonoBehaviour
{
    [SerializeField] private int row;
    private float counter = 0;
    
    private float speed = 0.5f;
    private float height = 0.5f;

    private bool toRight = true;

    [SerializeField] private GameObject startCheck;
    [SerializeField] private GameObject endCheck;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp (startCheck.transform.position, endCheck.transform.position, Mathf.PingPong(Time.time * speed, 1f));
        
    }
    
    

}
