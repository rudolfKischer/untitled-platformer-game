using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGrapple : MonoBehaviour
{
    Rigidbody2D body;
    public float grappleSpeed;
    private Vector2 grappleVelocity;
    private bool grappeling;
    // Start is called before the first frame update
    void Start()
    {
        grappleSpeed = 10;
        grappleVelocity = new Vector2(0,0);
        body = GetComponent<Rigidbody2D>();
        grappeling = false;
    }

    // Update is called once per frame
    public bool grappleCheck(){
        if(grappeling){
            return true;
        }else if(GetComponent<rayCastManager>().rayCastObject() != null && Input.GetKeyDown("g")){

            grappeling = true;
            return true;
        }else{
            return false;
        }
        //chec if currently grappeling 
        //check if keydown
        //check if rayHit a grappelable object
        
    return false;
    }

   public Vector2 grapple(){
        
        //check if grapel finsihed
        //set grappeling to false
        
 GameObject hitObject = GetComponent<rayCastManager>().rayCastObject();
  grappleVelocity =grappleSpeed*(hitObject.transform.position - transform.position ).normalized;
   return grappleVelocity;
    }

    
}
