using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NG_CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float gravityMultiplier;

    private NG_CharacterController _controller;
    private Rigidbody2D rb;
    private Vector2 _velocity;
        
    //private Animator anim;
    private Vector2 dir;

    [SerializeField] private bool isGrounded;

    public float MoveSpeed
    {
        get { return moveSpeed; }

        set { moveSpeed = value; }
    }

    public float JumpHeight
    {
        get { return jumpHeight; }

        set { jumpHeight = value; }
    }

    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<NG_CharacterController>();
        rb = GetComponent<Rigidbody2D>();
        //_velocity = this.gameObject.transform.
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 move = new Vector2(Input.GetAxis("Horizontal"),0);
        //_controller.Move(move * Time.deltaTime * moveSpeed);

        //Applying Gravity -NG
        _velocity += gravityMultiplier * Physics2D.gravity * Time.deltaTime;
        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 gravMove = Vector2.up * deltaPosition.y;

        //_controller.Move(_velocity * Time.deltaTime);
        

        Movement(gravMove);
    }


    private void Movement(Vector2 gravMove)
    {

    }
}
