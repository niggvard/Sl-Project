using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed; //speed applied to move formula
    [SerializeField] private GameObject player; // gameObject to reach player`s transform
    private Rigidbody2D playerBody; //assigned rigidbody
    private BoxCollider2D boxCollider; //assigned boxCollider
    [SerializeField] private LayerMask layerMask; //layer mask to check what raycast collided with
    private Controls input; //assigned keymaps of inputSystem
    private int jumpValue; //isJumped
    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();    
        input = new();
        input.Enable();
    }
    private void Update()
    {
        jumpValue = (int)input.PC.Jump.ReadValue<float>(); //gets the jumpButton keymap value
    }

    private void FixedUpdate()
    {
        short x = (short)input.PC.MoveX.ReadValue<float>(); //reads the MoveX axis values to implement them in move formula 
        short moveXform = (short)(x * speed * Time.fixedDeltaTime);

        if (isGrounded()) { playerBody.velocity = transform.up * jumpValue * 5f; } // regular jump

        playerBody.velocity = new Vector2(moveXform, playerBody.velocity.y); //move formula

        if (moveXform < 0) { player.transform.localScale = new Vector2(-1, 1); } //changes player direction
        else { player.transform.localScale = new Vector2(1, 1); }
    }
    private bool isGrounded() 
    {
        float extraHeight = .02f; //raycast height except radius of boxCollider
        RaycastHit2D rcHit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + extraHeight, layerMask); //raycast
        Color rayColor; //raycast color
        if (rcHit.collider != null) //raycast debug
        {
            rayColor = Color.red;
        }
        else 
        {
            rayColor = Color.blue;
        }
        Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.y + extraHeight), rayColor);
        return rcHit.collider != null; //returns t or f based on if grounded
    }
}
