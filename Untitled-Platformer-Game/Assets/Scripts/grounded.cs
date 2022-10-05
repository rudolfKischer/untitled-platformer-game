using UnityEngine;


public class grounded : MonoBehaviour
{
    private bool ground;
    private BoxCollider2D coll;
    

    [Header("Ground Layer")]
    [SerializeField][Tooltip("Which layers are read as the ground")] private LayerMask groundLayer;

    [Header("Collision Settings")]
    [SerializeField][Tooltip("Size of Ground Collider")] private float size = 0.01f;
    [SerializeField][Tooltip("Distance between collider and ")] private float distance = 0.0f; 

    [Header("Debug")]
    [SerializeField][Tooltip("Toggle all Debug options")] private bool debugToggle = false;
    
    [SerializeField][Tooltip("Toggle ")] private bool visualizeGroundCollider = false;


    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    
    private void Update(){
        Vector2 colSize = coll.bounds.size;
        Vector2 colPos = coll.bounds.center;
        float colVerticalSize = colSize.y; 
        colPos.y -= (colSize.y/2.0f) + distance;
        colSize.y = 0.01f;
        ground = Physics2D.BoxCast( colPos, colSize, 0.0f, Vector2.down, colSize.y, groundLayer);

        if(debugToggle && visualizeGroundCollider){
            debugUtils.visualizeBoxCast( colPos, colSize, 0.0f, Vector2.down, colVerticalSize * size, groundLayer);   
        }
    }

    public bool isGrounded() { 
        return ground;
    }
}