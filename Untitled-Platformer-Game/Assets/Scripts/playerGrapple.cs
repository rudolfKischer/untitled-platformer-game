using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGrapple : MonoBehaviour
{
    Rigidbody2D body;
    public float grappleSpringConstant = 0.65f;
    private Vector2 grappleVelocity;
    private bool grappling;

    private bool pressingGrapple;
    private GameObject grappleTarget;

    private Vector2 pointOnTarget;

    private rayCastManager rayCaster;

    private playerJump jump;

    public Transform tongue;
    public Transform tonguePivot;

    private playerWalk walk;

    private Camera camera;

    private bool isEnemy;
    
    public Animator anim;

    public GameObject musicManager;
    // Start is called before the first frame update
    void Start()
    {
        isEnemy = false;
        camera = GetComponent<Camera>();
        tonguePivot = transform.Find("TonguePivot");
        tongue = tonguePivot.Find("Tongue");
        body = GetComponent<Rigidbody2D>();
        rayCaster = GetComponent<rayCastManager>();
        jump = GetComponent<playerJump>();
        walk = GetComponent<playerWalk>();
        grappling = false;    
    }
    void Update(){
        //when g is pressed:
        //cast ray to mouse check if there is a grapleable object and if the player is already grappling
        Vector3 mouse = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 endRay =  mouse - transform.position;
        Debug.DrawRay(transform.position + 2f*endRay.normalized, endRay, Color.green);
        pressingGrapple = Input.GetKey("g");
        if(!grappling && Input.GetKeyDown("g") && rayCaster.rayCastObject() && (rayCaster.rayCastObject().GetComponent<grappleSettings>() != null)){
            grappleTarget = rayCaster.rayCastObject();
            Vector2 targetPos = grappleTarget.transform.position;
            pointOnTarget = rayCaster.rayCastPoint() - targetPos;
            grappling = true;
            isEnemy = grappleTarget.GetComponent<grappleSettings>().isEnemy;
           
           


        }

       updateMusic();
       animate();
    }
private void animate(){
    if(grappling){
    anim.SetBool("isGrappling",true);
    }else{
      anim.SetBool("isGrappling",false);  
    }
}
private void updateMusic(){
     if(grappling){
            musicManager.GetComponent<musicManager>().trackOn(0);
        }else {
            if(GetComponent<grounded>().isGrounded()){
            musicManager.GetComponent<musicManager>().trackOff(0);
            }
        }
}
    private float clampForceVal(float force, float velocityGoal){
        //responsible for making sure the acceleration doesnt over shoot
        float speedDiff = (velocityGoal - body.velocity.x);
        float maxForce = speedDiff / Time.deltaTime;
        
        if(maxForce > 0.0f && force > 0.0f){
            return Mathf.Min(force, maxForce);
        }else if(maxForce < 0.0f && force < 0.0f){
            return Mathf.Max(force, maxForce);
        }else if(force == 0.0f){
            return force;
        }else {
            return maxForce;
        }
    }

    private void drawGrapple(){
        
        if(grappling){
            tongue.GetComponent<Renderer>().enabled = true;
            float angle = Vector2.SignedAngle(new Vector2(0.0f,1.0f), grappleDirection());
            tonguePivot.localRotation = Quaternion.Euler(0, 0, angle);
            tonguePivot.localScale = new Vector3(tonguePivot.localScale.x, grappleDistance() * 0.25f, tonguePivot.localScale.z);
        }else{         
            tongue.GetComponent<Renderer>().enabled = false;
        }
    }

    private Vector2 getGrapplePoint(){
        Vector2 targetPos = grappleTarget.transform.position;
        return targetPos + pointOnTarget;
    }

    private Vector2 grappleDisplacement(){
        return (getGrapplePoint() - body.position);
    }

    private float grappleDistance(){
        return grappleDisplacement().magnitude;
    }

    private Vector2 grappleDirection(){
        return grappleDisplacement().normalized;
    }

    private bool grappledToPosition(){
        float threshold = 3.0f;
        return grappleDistance() < threshold;
    }

    public Vector2 getGrappleForce(){
        //send velocity
        //check if grapel finsihed
        //set grappeling to false
        Vector2 grappleVelocity = new Vector2(0,0);
        if(grappling){
            float MaxPullDistance = 10;
            float MinPullDistance = 3;
            if(grappleDistance() > MinPullDistance){
                float minimizedDistance = Mathf.Min(grappleDistance(), MaxPullDistance);
                grappleVelocity = grappleSpringConstant * grappleDirection() * minimizedDistance;
            }    
        }
        if(!pressingGrapple){
            grappling = false;
        }
        
        
        return grappleVelocity;
    }

   public Vector2 getNetForce(){
        Vector2 netForce = new Vector2(0.0f,0.0f);
        netForce += getGrappleForce();
        return netForce;
   }
public float getRelativeMassCoefficient(){
    //implament frog mass
    float frogMass = getFrogMass();
    float grappleTargetMass = getGrappleTargetMass();
    float relativeMassCoefficient = grappleTargetMass/frogMass;
    return relativeMassCoefficient;
//if mass is more than target due defualt
//if target is equal apply to both
//if mass is less than target pull towards

}

public float getFrogMass(){
    return this.GetComponent<movementController>().mass;

}
public float getGrappleTargetMass(){
return grappleTarget.GetComponent<grappleSettings>().mass;
}

public bool compareVector2Positions(float offset){
    return  grappling && grappleTarget.GetComponent<Rigidbody2D>().position.x < body.position.x +offset && grappleTarget.GetComponent<Rigidbody2D>().position.x > body.position.x -1* offset
            && grappleTarget.GetComponent<Rigidbody2D>().position.y < body.position.y +offset && grappleTarget.GetComponent<Rigidbody2D>().position.y > body.position.y -1*offset;

}
public float calculateOffset(){
return 2*getFrogMass();
}
public void eat(){
            if(getRelativeMassCoefficient() < 1 && compareVector2Positions(calculateOffset())){
                Destroy(grappleTarget);
                isEnemy = false;
                grappling = false;
                anim.SetTrigger("eat");
                this.GetComponent<movementController>().increaseMass(getGrappleTargetMass());
            }
}

   private void FixedUpdate()
    {   
        
        drawGrapple();
        if(isEnemy){
          
            grappleTarget.GetComponent<Rigidbody2D>().velocity += -1 * (1 / getRelativeMassCoefficient()) * getNetForce();
            body.velocity += getRelativeMassCoefficient() / 2 * getNetForce();
            eat();
            
            }else{
            body.velocity += getNetForce(); 
        }
    }

    
}