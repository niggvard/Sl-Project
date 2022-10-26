using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D playerBody;
    private Controls input;

    private void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        input = new();
        input.Enable();
    }

    private void FixedUpdate()
    {
        short x = (short)input.PC.MoveX.ReadValue<float>();

        playerBody.velocity = new Vector2(x * speed * Time.fixedDeltaTime, playerBody.velocity.y);
    }
}
