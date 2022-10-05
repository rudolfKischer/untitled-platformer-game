using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGrapple : MonoBehaviour
{
    Rigidbody2D body;
    public float grappleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        grappleSpeed = 10;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Raycasting
        Vector3 endRay = Input.mousePosition - transform.position;
        Debug.DrawRay(transform.position + 2f*endRay.normalized, endRay, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position + 2f*endRay.normalized, endRay, 10f);
        
        if(hit.collider != null){
        
        body.velocity = grappleSpeed*(hit.collider.gameObject.transform.position - transform.position ).normalized;
        Debug.Log(body.velocity);
       } 
    }
}
