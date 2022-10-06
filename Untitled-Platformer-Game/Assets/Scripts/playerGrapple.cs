using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGrapple : MonoBehaviour
{
    Rigidbody2D body;
    public float grappleSpeed;
    private Vector2 grappleVelocity;
    public bool grappling;
    private bool gUp;
    private GameObject grappleTarget;
    // Start is called before the first frame update
    void Start()
    {
        gUp = false;
        grappleSpeed = 10;
        grappleVelocity = new Vector2(0,0);
        body = GetComponent<Rigidbody2D>();
        grappling = false;
        grappleTarget = null;
    }
    void Update(){
        //when g is pressed:
        //cast ray to mouse check if there is a grapleable object and if the player is already grappling
         Vector3 endRay = Input.mousePosition - transform.position;
        Debug.DrawRay(transform.position + 2f*endRay.normalized, endRay, Color.green);
        
        if(!grappling && Input.GetKeyDown("g") && GetComponent<rayCastManager>().rayCastObject()){
            grappleTarget = GetComponent<rayCastManager>().rayCastObject();
            grappling = true;
         Debug.Log("Hello");
          
         
        }
    }

   public Vector2 grapple(){
        //send velocity
        //check if grapel finsihed
        //set grappeling to false
        if((grappleTarget.transform.position - transform.position ).x > 3 ||
         (grappleTarget.transform.position - transform.position ).y > 3 ) {    
            grappleVelocity =grappleSpeed*(grappleTarget.transform.position - transform.position ).normalized;
            return grappleVelocity;
        }else{
            grappling = false;
            return new Vector2(0,0);
        }
   }

   private void FixedUpdate()
    {
        if(grappling){
            // Debug.Log("fuck lawyers");
            body.velocity += grapple();
        }
    }

    
}
