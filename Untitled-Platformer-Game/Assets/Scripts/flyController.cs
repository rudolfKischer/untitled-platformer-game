using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyController : MonoBehaviour
{
   Rigidbody2D body;
   public float flySpeedHorizontal;
   public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
   

    }
     private void FixedUpdate()
    {   
        
      
    }

}
