using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCastManager : MonoBehaviour
{   
    
    
    public GameObject rayCastObject(){
        // Debug.Log("Casting!");
        Vector3 mouse = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector3 endRay = mouse - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + 2f*endRay.normalized, endRay, 50f);
       if(hit.collider != null){
           return hit.collider.gameObject;
       }else{
           return null;
       }
        
        
        
        

    }

    public Vector2 rayCastPoint(){
        Vector3 mouse = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector3 endRay = mouse - transform.position;
        Debug.DrawRay(transform.position + 2f*endRay.normalized, endRay, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + 2f*endRay.normalized, endRay, 50f);
        if (hit.collider != null)
        {
            //Display the point in world space where the ray hit the collider's surface.
            return hit.point;
        }else{
            return transform.position;
        }
    }

   
    
}
