using UnityEngine;


public class grounded : MonoBehaviour
{
    private bool ground;
    private BoxCollider2D coll;
    

    [Header("Ground Layer")]
    [SerializeField][Tooltip("Which layers are read as the ground")] private LayerMask groundLayer;

    [Header("Collision Settings")]
    [SerializeField][Tooltip(" Ground Collider Scale compared to Box collider")] private float size = 0.2f;
    [SerializeField][Tooltip("Distance between collider and ")] private float distance = 0.0f; 

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        Vector2 colSize = coll.bounds.size;
        Vector2 colPos = coll.bounds.center;
        float colVerticalSize = colSize.y; 
        colSize.y = 0.1f;
        colPos.y -= (colSize.y/2.0f) + distance;  //(colSize.y/2.0f)
        ground = Physics2D.BoxCast( colPos, colSize, 0.0f, Vector2.down, colVerticalSize * size, groundLayer);   
    }

    public bool isGrounded() { 
        return ground;
    }
}