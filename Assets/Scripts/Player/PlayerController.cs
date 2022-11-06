using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed; //speed applied to move formula
    [SerializeField] private LayerMask layerMask; //layer mask to check what raycast collided with
    [SerializeField] private Animator an; //plaer animator
    private Rigidbody2D playerBody; //assigned rigidbody
    private Controls input; //assigned keymaps of inputSystem

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();  
        input = new();
        input.Enable();
    }

    private void FixedUpdate()
    {
        short x = (short)input.PC.MoveX.ReadValue<float>(); //reads the MoveX axis values to implement them in move formula 
        short moveXform = (short)(x * speed * Time.fixedDeltaTime);

        playerBody.velocity = new Vector2(moveXform, playerBody.velocity.y); //move formula

        if (moveXform > 0 || moveXform < 0) { an.SetBool("walk", true); }//animation trigger
        else { an.SetBool("walk", false); }

        if (moveXform < 0) { playerBody.transform.localScale = new Vector2(-1, 1); } //changes player direction
        else if(moveXform > 0) { playerBody.transform.localScale = new Vector2(1, 1); }
    }
}
