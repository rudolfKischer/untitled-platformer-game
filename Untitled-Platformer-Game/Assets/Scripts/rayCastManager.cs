using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCastManager : MonoBehaviour
{   
    
    
    public GameObject rayCastObject(){

        Vector3 endRay = Input.mousePosition - transform.position;
        Debug.DrawRay(transform.position + 2f*endRay.normalized, endRay, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + 2f*endRay.normalized, endRay, 10f);
        
        
          
        
        return hit.collider.gameObject;
        
        
        

    }

   
    
}
