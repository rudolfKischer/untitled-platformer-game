using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : MonoBehaviour
{
    float horizontal;
    float vertical;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if(vertical == 1){
            anim.SetBool("isJumping", true);
        }else{
       
            anim.SetBool("isJumping", false);
        }
        
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
       Debug.Log(horizontal);
       // Debug.Log(anim.GetBool("isJumping"));
    }
}
