using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject groundChecker;
    private Rigidbody2D playerBody;
    private Controls input;
    private int jumpValue;
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
        short x = (short)input.PC.MoveX.ReadValue<float>();
        playerBody.AddForce(transform.up * jumpValue * 5f, ForceMode2D.Impulse);


        playerBody.velocity = new Vector2(x * speed * Time.fixedDeltaTime, playerBody.velocity.y);
    }
}
