using UnityEngine;

public class playerJump : MonoBehaviour
{
    //vertical Movement
    [Header("Speed")]
    [SerializeField][Tooltip("Velocity given when jump pressed")] public float jumpForce = 1500.0f;
    [SerializeField][Tooltip("increases Jump height while holding")] float holdJumpStrength = 0.25f; // should be in between 
    [SerializeField][Tooltip("Max vertical velocity")] public float terminalVelocity = 100.0f;

    [Header("Gravity")]
    [SerializeField][Tooltip("Strength of gravity while jumping up")] public float gForceUp = -50.00f;
    [SerializeField][Tooltip("Strength of gravity while falling")] public float gForceDown = 200.0f;
    
    
    
    [HideInInspector] public Rigidbody2D body;
    private grounded grounded;
    private bool jumpRequested;
    private float vertical; //vertical control
    
    //Jump Control

    void Awake()
    {
         grounded = GetComponent<grounded>();
    }
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private bool holdingJump(){
        return vertical > 0;
    }

    private bool isGrounded(){
        return grounded.isGrounded();
    }
    private bool goingUp(){
        return body.velocity.y > 0;
    }

    private float getGravityForce(){
        //change up vs down gravity
        if(goingUp()){
            return gForceUp;
        }else{
            return gForceDown;
        }
    }

    private float getNormalForce(){
        if(isGrounded()){
            return getGravityForce();
        }
        return 0.0f;
    }

    private float getJumpForce(){
        if(jumpRequested && isGrounded()){
            jumpRequested = false; //might not be ideal to have this here
            return jumpForce;
        }
        return 0.0f;
    }

    private float getHoldJumpForce(){
        if(holdingJump() && goingUp()){
            return -getGravityForce() * holdJumpStrength;
        }
        return 0;
    }

    private float getVerticalNetForce(){
        float netVerticalForce = 0;
        netVerticalForce += getGravityForce();
        netVerticalForce += getNormalForce();
        netVerticalForce += getJumpForce();
        netVerticalForce += getHoldJumpForce();
        return netVerticalForce;
        
    }


    public Vector2 getDeltaVelocity(){
        float deltaVerticalVelocity = getVerticalNetForce() * Time.deltaTime;
        return new Vector2(body.velocity.x, deltaVerticalVelocity);
    }

     void Update()
    {
        vertical = Input.GetAxisRaw("Jump");
        if(Input.GetKeyDown(KeyCode.Space)){
            jumpRequested = true;
        }
    }
    private void FixedUpdate()
    { 
        body.velocity += getDeltaVelocity();
    }





}