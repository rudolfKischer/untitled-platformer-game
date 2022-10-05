using UnityEngine;

public class playerJump : MonoBehaviour
{
    //vertical Movement
    [] float holdJumpStrength = 1.0f;
    public float jumpSpeed = 20.0f;
    public float upGravity = -2.00f;
    public float downGravity = -3.0f;
    public float terminalVelocity = 100.0f;
    private bool jumpRequested;


    private void fall(){
        //change up vs down gravity
        float downAcceleration;
        if(verticalVelocity > 0){
            downAcceleration = upGravity / (1+holdJumpStrength*vertical) ;
        }else{
            downAcceleration = downGravity;
        }

        if( Mathf.Abs(verticalVelocity) < terminalVelocity){
            verticalVelocity += downAcceleration;
        }else{
            verticalVelocity = Mathf.Sign(verticalVelocity) * terminalVelocity ;
        }
        
    }

    private void jump(){
            verticalVelocity += vertical * jumpSpeed;
    }



    public void updateJump(){
        if(grounded.isGrounded()){
            verticalVelocity = 0;
            if(jumpRequested){
                jump();
                jumpRequested = false;
            }
        }else{
            fall();
        }
    }


    void void Awake()
    {
        
    }
    void Start()
    {

    }

     void Update()
    {

    }
    private void FixedUpdate()
    { 

    }





}