using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class movementController : MonoBehaviour
{

    public LayerMask groundLayer;
    Rigidbody2D body;
    private BoxCollider2D coll;

    public Animator anim;

    //controls
    float horizontal;
    float vertical;
    bool dashing;

    float onGround = 1;

    
    

    //vertical Movement
    public float holdJumpStrength = 1.0f;
    public float jumpSpeed = 20.0f;
    public float upGravity = -2.00f;
    public float downGravity = -3.0f;

    //horizonal movement
    public float dashSpeed = 40.0f;
    public float dashTime = 0.2f;
    public float dashCoolDown = 0.3f;
    public float dashTimer = 0.0f;

    public float horizontalVelocity = 0.0f;
    public float verticalVelocity = 0.0f;
    public float runSpeed = 10.0f;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
       //Raycasting
        Vector3 endRay = Input.mousePosition - transform.position;
       Debug.DrawRay(transform.position + 2f*endRay.normalized, endRay, Color.green);
       RaycastHit2D hit = Physics2D.Raycast(transform.position + 2f*endRay.normalized, endRay, 10f);
       if(hit.collider != null){
        
        Debug.Log (hit.collider.gameObject);
       } 

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Jump");
        if(dashTimer < dashCoolDown && Input.GetKeyDown(KeyCode.LeftShift)){
            dashTimer = dashTime;
        }else{
            dashTimer -= Time.deltaTime;
            if(dashTimer < 0.0f){
                dashTimer = 0.0f;
            }
          
        } 
       
        animate();
        

        
    }

    private void OnCollisionStay2D(Collision2D collision){
        
        //Debug.Log(collision);

        if (collision.gameObject.layer== groundLayer){
            if(isGrounded()){
                onGround = 1;
            }
        }
 
    }

    



    bool isGrounded() {
        Vector2 size = coll.bounds.size;
        return Physics2D.BoxCast(coll.bounds.center, size, 0f, Vector2.down, 0.1f, groundLayer);
    }

    private void dash(){
        if(dashTimer > 0){
            horizontalVelocity += horizontal * dashSpeed;
        }
    }

    private void jump(){
        if(!isGrounded()){

            //change up vs down gravity
            float downAcceleration;
            if(verticalVelocity > 0){
                downAcceleration = upGravity / (1+holdJumpStrength*vertical) ;
            }else{
                downAcceleration = downGravity;
            }

            verticalVelocity += downAcceleration;

        }else{
            verticalVelocity = 0;
            verticalVelocity += vertical * jumpSpeed;
        }

    }

    private void animate(){
        
        

        if(horizontal >= 0.5){
            anim.SetBool("isMovingRight", true);
        }else{
       
            anim.SetBool("isMovingRight", false);
        }
        if(horizontal <= -0.5){
            anim.SetBool("isMovingLeft", true);
        }else{
       
            anim.SetBool("isMovingLeft", false);
        }

        if(vertical == 1){
            anim.SetBool("isJumping", true);
        }else{
       
            anim.SetBool("isJumping", false);
        }
        if(Random.Range(1,1000) == 253){
            anim.SetTrigger("Lick");
        }

    }
    

    //updates with time
    private void FixedUpdate()
    {   
        horizontalVelocity = horizontal * runSpeed;
        jump();
        dash();
        body.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        
    }


}
