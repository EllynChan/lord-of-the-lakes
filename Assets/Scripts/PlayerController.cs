using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        handleMove();
    }

    void ProcessInputs()
    {
        float horizontalInput = Input.GetKey(KeyCode.D) ? 1f : Input.GetKey(KeyCode.A) ? -1f : 0f;
        float verticalInput = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0f;
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
    }

    void handleMove()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
