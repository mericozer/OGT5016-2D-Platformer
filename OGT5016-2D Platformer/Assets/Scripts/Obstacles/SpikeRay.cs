using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRay : MonoBehaviour
{
    //checks the movement of the spikes with the help of ray check
    
    private Animator _animator;
    
    private bool toLeft = false;
    private bool isMoving = true;

    [SerializeField]private float moveSpeed = 5f;
    [SerializeField] private float heightOffset = 3f;

    [SerializeField] private LayerMask groundLayerMask;
    
    [SerializeField] private Transform groundChecker;
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            
            //flips rotation when left or right way ends
            if (!IsGround())
            {
                if (toLeft)
                {
                    transform.rotation = Quaternion.Euler(0 , 0, 0);
                    toLeft = false;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0 , 180, 0);
                    toLeft = true;
                }
            }
        }

        

    }
    private bool IsGround()
    {
       
        RaycastHit2D rayCastHit= Physics2D.Raycast(groundChecker.position, 
            Vector2.down, 
            heightOffset,
            groundLayerMask);

        Color rayColor;
        if (rayCastHit.collider != null)
        {
            rayColor = Color.blue;
        }
        else
        {
            rayColor = Color.green;
        }
        Debug.DrawRay(groundChecker.position, Vector2.down *  + heightOffset, rayColor);
        

        return rayCastHit.collider != null;
    }

    public void Stop()
    {
        isMoving = false;
    }
    
}
