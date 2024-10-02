using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator _animator;
    private BoxCollider2D collider;
    private SpriteRenderer renderer;
    private StateManager stateManager;
    
    [SerializeField] private Sprite[] sprites; 

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    private readonly Vector2 colliderSizeHorizontal = new Vector2(3.7462f, 1.2828f);
    private readonly Vector2 colliderSizeVertical = new Vector2(1.8523f, 2.5828f);
    private readonly Vector2 colliderOffsetHorizontal = new Vector2(0.2165f, -0.9760f);
    private readonly Vector2 colliderOffsetVertical = new Vector2(0.0119f, -0.5760f);

    public Vector2 moveDirection;
    public float moveSpeed;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        moveSpeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        ProcessCollider();
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

    void ProcessCollider() {
        if (renderer.sprite == sprites[0] || renderer.sprite == sprites[1]) {
            collider.size = colliderSizeVertical;
            collider.offset = colliderOffsetVertical;
        } else {
            collider.size = colliderSizeHorizontal;
            collider.offset = colliderOffsetHorizontal;
        }
    }

    void handleMove()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
