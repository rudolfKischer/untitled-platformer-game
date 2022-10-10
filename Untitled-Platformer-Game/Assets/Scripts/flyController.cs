using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyController : MonoBehaviour
{
   Rigidbody2D body;
   public float SpeedHorizontal;
   public Animator anim;
   public float timeTurn;
   private float timerTurn;
   public int directionX;
   public float walkForce;
   public Transform Player;
   public GameObject musicManager;
    // Start is called before the first frame update
    void Start()
    {
        directionX = 1;
        body = GetComponent<Rigidbody2D>();
        timerTurn = timeTurn;
    }

    // Update is called once per frame
    void Update()
    {
        

//countDownTurnTimer();
chasePlayer();
musicChanger();
animate();




    }

private void musicChanger(){
    if(Player.position.x - body.position.x < 20 && Player.position.x - body.position.x > -20){
musicManager.GetComponent<musicManager>().trackOn(4);
    }else{
musicManager.GetComponent<musicManager>().trackOff(4);
    }

    
}
private void chasePlayer(){

if(Player.position.x - body.position.x <0){
    directionX = -1;


}else{
    directionX = 1;
}



    }




private void animate(){
if(directionX == 1){
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
 directionX = directionX*-1;

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
        float currentWalkForce = clampForceVal(walkForce /2.0f * directionX, 5*directionX);
        return currentWalkForce;
 }
 
private float getFlyHorizontal(){


    
return SpeedHorizontal*directionX;

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
