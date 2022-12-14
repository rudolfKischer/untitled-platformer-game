using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class movementController : MonoBehaviour
{

    [Header("Components")]
    [HideInInspector]public Rigidbody2D body;
    private grounded grounded;
    private BoxCollider2D coll;

    public Animator anim;
    public float mass;
    
    //controls
    float horizontal;
    float vertical;
    bool dashing;

    //horizonal movement
    public float dashSpeed = 40.0f;
    public float dashTime = 0.2f;
    public float dashCoolDown = 0.3f;
    private float dashTimer = 0.0f;

    public float horizontalVelocity = 0.0f;
    public float verticalVelocity = 0.0f;
    public float runSpeed = 10.0f;
    
    void Awake(){
        grounded = GetComponent<grounded>();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
     
        
   

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

    public void increaseMass(float enemyMass){
    mass += enemyMass;
    this.transform.localScale += new Vector3(enemyMass, enemyMass, 0);

    }

    private void dash(){
        if(dashTimer > 0){
            horizontalVelocity += horizontal * dashSpeed;
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

        if(vertical == 1 && !grounded.isGrounded()){
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
        // horizontalVelocity = horizontal * runSpeed;
        dash();
        // body.velocity -= (body.velocity - new Vector2(horizontalVelocity,  body.velocity.y));
        
    }


}
