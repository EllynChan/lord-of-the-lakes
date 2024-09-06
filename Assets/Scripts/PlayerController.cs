using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    public Vector2 moveDirection;
    public float moveSpeed;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        moveSpeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        handleMove();
        _animator.SetFloat(_horizontal, moveDirection.x);
        _animator.SetFloat(_vertical, moveDirection.y);

        if (moveDirection != Vector2.zero)
        {
            Debug.Log(moveDirection);
            _animator.SetFloat(_lastHorizontal, moveDirection.x);
            _animator.SetFloat(_lastVertical, moveDirection.y);
        }

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
