using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed; //speed applied to move formula
    public bool isMoving = true; //represent player ability to move;
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

        short xPc = (short)input.PC.MoveX.ReadValue<float>(); //reads the MoveX axis values to implement them in move formula for pc
        short xXbox = (short)input.Xbox.MoveX.ReadValue<float>();

        short moveXform = (short)(Mathf.Clamp(xPc + xXbox, -1, 1) * speed * Time.fixedDeltaTime);

        if(isMoving) playerBody.velocity = new Vector2(moveXform, playerBody.velocity.y); //move formula

        if (moveXform > 0 && isMoving || moveXform < 0 && isMoving) { an.SetBool("walk", true); }//animation trigger
        else { an.SetBool("walk", false); }

        if (moveXform < 0 && isMoving) { playerBody.transform.localScale = new Vector2(-1, 1); } //changes player direction
        else if(moveXform > 0 && isMoving) { playerBody.transform.localScale = new Vector2(1, 1); }
    }
}
