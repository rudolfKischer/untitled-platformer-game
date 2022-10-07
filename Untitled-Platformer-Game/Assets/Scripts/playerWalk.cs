using UnityEngine;

public class playerWalk : MonoBehaviour
{
    [Header("Walk Strength")]
    [SerializeField][Tooltip("Strength of acceleration to get up to walk speed")] public float walkForce = 200.0f;

    [SerializeField][Tooltip("Strength of acceleration to speed while in air")] public float airWalkForce = 200.0f;
    
    [Header("Horizontal Speed Caps")]
    [SerializeField][Tooltip("Max horizontal walk velocity")] public float walkSpeed = 30.0f;

    [SerializeField][Tooltip("Max horizontal airWalk velocity")] public float airWalkSpeed = 30.0f;

    [SerializeField][Tooltip("Max horizontal velocity")] public float terminalHorizontalVelocity = 100.0f;

    [HideInInspector] public Rigidbody2D body;
    private grounded grounded;

    float horizontal;
    void Awake(){
        grounded = GetComponent<grounded>();
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private bool isGrounded(){
        return grounded.isGrounded();
    }

    private bool isMoving(){
        return Mathf.Abs(body.velocity.x) > 0.0f;
    }

    private bool isWalking(){
        return horizontal != 0.0f && isGrounded() && isMoving(); 
    }

    private float clamp(float val, float min, float max){
        //returns the a value within a limit
        if(val < min){
            return min;
        }
        if(val > max){
            return max;
        }
        return val;
    }

    private float clampVelocity(float velocity){
        //returns max the velocity within the terminal limit
        return clamp(velocity, -terminalHorizontalVelocity, terminalHorizontalVelocity);
    }
    
    private float clampForceVal(float force, float velocityGoal){
        //responsible for making sure the acceleration doesnt over shoot
        float speedDiff = (velocityGoal - body.velocity.x);
        float maxForce = speedDiff / Time.deltaTime;

        if(maxForce > 0 && force > 0){
            return Mathf.Min(force, maxForce);
        }else if(maxForce < 0 && force < 0){
            return Mathf.Max(force, maxForce);
        }else{
            return maxForce;
        }
    }

    private float getWalkForce(){
        float currentWalkForce = clampForceVal(walkForce * horizontal,walkSpeed * horizontal);
        if(isGrounded()){
            return currentWalkForce;
        }
        return 0;
    }

    private float getAirWalkForce(){
        float airWalkMaxVelocity = airWalkSpeed  * horizontal;
        float airWalkDirectionalForce = airWalkForce * horizontal;
        float currentAirWalkForce = clampForceVal(airWalkDirectionalForce,airWalkMaxVelocity);
        if(!isGrounded() ){
            return currentAirWalkForce;
        }
        return 0;
    }

    private float getNetHorizontalForce(){
        float netHorizontalForce=0;
        netHorizontalForce += getWalkForce();
        netHorizontalForce += getAirWalkForce();
        return netHorizontalForce;
    }

    public Vector2 getDeltaVelocity(){
        float deltaHorizontalVelocity = getNetHorizontalForce() * Time.deltaTime; 
        return new Vector2(deltaHorizontalVelocity, 0.0f);
    }

    private void FixedUpdate()
    { 
        body.velocity += getDeltaVelocity();
        float clampedVelocity = clampVelocity(body.velocity.x);
        body.velocity = new Vector2(clampedVelocity, body.velocity.y); //make sure does not surpass terminal velocity
    }


}