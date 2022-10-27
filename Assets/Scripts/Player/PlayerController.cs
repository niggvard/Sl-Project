using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed; //speed applied to move formula
    [SerializeField] private LayerMask layerMask; //layer mask to check what raycast collided with
    [SerializeField] private BoxCollider2D GroundedBoxCollider;
    private Rigidbody2D playerBody; //assigned rigidbody
    private Controls input; //assigned keymaps of inputSystem
    private int jumpValue; //isJumped

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();  
        input = new();
        input.Enable();
    }
    private void Update()
    {
        jumpValue = (int)input.PC.Jump.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        short x = (short)input.PC.MoveX.ReadValue<float>(); //reads the MoveX axis values to implement them in move formula 
        short moveXform = (short)(x * speed * Time.fixedDeltaTime);

        if (IsGrounded()) { playerBody.velocity = transform.up * jumpValue * 5f; } // regular jump

        playerBody.velocity = new Vector2(moveXform, playerBody.velocity.y); //move formula

        if (moveXform < 0) { playerBody.transform.localScale = new Vector2(-1, 1); } //changes player direction
        else { playerBody.transform.localScale = new Vector2(1, 1); }
    }
    private bool IsGrounded() 
    {
        return Physics2D.IsTouchingLayers(GroundedBoxCollider, layerMask.value);
    }
}
