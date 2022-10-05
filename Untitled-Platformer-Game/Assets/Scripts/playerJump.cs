using UnityEngine;

public class playerJump : MonoBehaviour
{
    //vertical Movement
    [Header("Speed")]
    [SerializeField][Tooltip("Velocity given when jump pressed")] public float jumpSpeed = 20.0f;
    [SerializeField][Tooltip("increases Jump height while holding")] float holdJumpStrength = 1.0f;
    [SerializeField][Tooltip("Max vertical velocity")] public float terminalVelocity = 100.0f;

    [Header("Gravity")]
    [SerializeField][Tooltip("Strength of gravity while jumping up")] public float upGravity = -2.00f;
    [SerializeField][Tooltip("Strength of gravity while falling")] public float downGravity = -3.0f;
    
    
    
    [HideInInspector] public Rigidbody2D body;
    private grounded grounded;
    private bool jumpRequested;
    private float verticalVelocity;
    private float vertical;
    
    //Jump Control

    void Awake()
    {
         grounded = GetComponent<grounded>();
    }
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

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
        verticalVelocity += jumpSpeed;
    }


    public void updateJump(){
        if(grounded.isGrounded()){
            verticalVelocity = 0;
            if(jumpRequested){
                jump();
                
            }
        }else{
            fall();
        }
        jumpRequested = false;
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

        updateJump();
        body.velocity = new Vector2(body.velocity.x, verticalVelocity);
    }





}