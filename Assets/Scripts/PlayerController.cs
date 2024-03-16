using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    //private Collider2D collider;
    private Animator animator;

    public float runSpeed = 5.0f;

    private bool isActive = true;

    Vector2 moveDirection;
    Vector2 lastMoveDirection;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        //collider = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isActive)
        {
            float horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
            float vertical = Input.GetAxisRaw("Vertical"); // -1 is down

            bool stoppedMoving = horizontal == 0 && vertical == 0;

            bool lastMoveDirectionMoved = moveDirection.x != 0 || moveDirection.y != 0;

            if (stoppedMoving && lastMoveDirectionMoved)
            {
                lastMoveDirection = moveDirection;
            }

            moveDirection = new Vector2(horizontal, vertical);
        }
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            body.velocity = new Vector2(moveDirection.x * runSpeed, moveDirection.y * runSpeed);

            UpdateAnimation();
        }
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("horizontalMovement", moveDirection.x);
        animator.SetFloat("verticalMovement", moveDirection.y);
        animator.SetFloat("lastHorizontalMovement", lastMoveDirection.x);
        animator.SetFloat("lastVerticalMovement", lastMoveDirection.y);
        animator.SetFloat("movementMagnitude", moveDirection.magnitude);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Bomb")
        {
            collision.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Explosion")
        {
            animator.SetBool("isDead", true);
        }
    }
}
