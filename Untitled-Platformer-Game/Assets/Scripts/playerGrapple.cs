using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGrapple : MonoBehaviour
{
    Rigidbody2D body;
    public float grappleSpeed;
    private Vector2 grappleVelocity;
    private bool grappeling;
    private GameObject grappleTarget;
    // Start is called before the first frame update
    void Start()
    {
        grappleSpeed = 10;
        grappleVelocity = new Vector2(0,0);
        body = GetComponent<Rigidbody2D>();
        grappeling = false;
        grappleTarget = null;
    }

    // Update is called once per frame
    public bool grappleCheck(){
        
        if(grappeling){
            return true;
        }else if(Input.GetKeyDown("g")){
            if(GetComponent<rayCastManager>().rayCastObject() != null){
            

            grappleTarget = GetComponent<rayCastManager>().rayCastObject();
            grappeling = true;
            return true;
        }
        }else{
            grappleTarget = null;
            return false;
        }
        //chec if currently grappeling 
        //check if keydown
        //check if rayHit a grappelable object
        
    return false;
    }

   public Vector2 grapple(){
    Debug.Log("helloWorld");
        //send velocity
        //check if grapel finsihed
        //set grappeling to false
  if((grappleTarget.transform.position - transform.position ).x > 1 || (grappleTarget.transform.position - transform.position ).y > 1 ) {    
  grappleVelocity =grappleSpeed*(grappleTarget.transform.position - transform.position ).normalized;
   return grappleVelocity;
    }else{
        grappeling = false;
        return new Vector2(0,0);
    }
   }

    
}
