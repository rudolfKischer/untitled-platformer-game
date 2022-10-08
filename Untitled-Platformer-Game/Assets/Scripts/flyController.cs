using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyController : MonoBehaviour
{
   Rigidbody2D body;
   public float flySpeedHorizontal;
   public Animator anim;
   public float timeTurn;
   private float timerTurn;
   public int direction;
   private bool directionChanged;
   public float walkForce;
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        body = GetComponent<Rigidbody2D>();
        timerTurn = timeTurn;
    }

    // Update is called once per frame
    void Update()
    {
        

countDownTurnTimer();

animate();
    }
private void animate(){

if(direction == 1){
    anim.SetBool("isMovingRight", true);
    anim.SetBool("isMovingLeft", false);
}else{
    anim.SetBool("isMovingLeft", true);
    anim.SetBool("isMovingRight", false);
}

    
}
private void countDownTurnTimer(){
    if(timerTurn>0){
timerTurn -= Time.deltaTime;

}else {
    timerTurn = timeTurn;
    changeDirection();
}
}
  
private void changeDirection(){
 direction = direction*-1;
 directionChanged = true;

}

private float swapedDirection(){
 if(directionChanged){
    
    directionChanged = false;
    return 10*timeTurn*direction;
 }else{
    return 0;
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

 private float getFrictionForce(){
        float currentWalkForce = clampForceVal(walkForce /2.0f * direction, 5*direction);
        return currentWalkForce;
 }
private float getFlyHorizontal(){


    
return flySpeedHorizontal*direction;

  }

    
    private float getNetHorizontalForce(){
        float netHorizontalForce=0;
        netHorizontalForce += getFlyHorizontal();

        netHorizontalForce += getFrictionForce();
        return netHorizontalForce;
    }

    public Vector2 getDeltaVelocity(){
        float deltaHorizontalVelocity = getNetHorizontalForce() * Time.deltaTime; 
        return new Vector2(deltaHorizontalVelocity, 0.0f);
    }

    private void FixedUpdate()
    { 
        
        body.velocity += getDeltaVelocity();
    }

}
