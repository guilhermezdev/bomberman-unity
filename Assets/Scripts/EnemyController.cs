using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Vector2 moveDirection;

    private Rigidbody2D body;
    private Animator animator;

    public float runSpeed = 5.0f;

    public Vector2 startMovement = Vector2.down;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
        moveDirection = startMovement;
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        {
            body.velocity = new Vector2(moveDirection.x * runSpeed, moveDirection.y * runSpeed);

            if (moveDirection.y != 0f)
            {
                transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
            }
            else if (moveDirection.x != 0f)
            {
                transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y));
            }

        }
    }

    private void UpdateAnimation()
    {

        if(moveDirection == Vector2.down)
        {
            animator.SetFloat("verticalMovement", -1);
            animator.SetFloat("horizontalMovement", 0);
        }else if(moveDirection == Vector2.up)
        {
            animator.SetFloat("verticalMovement", 1);
            animator.SetFloat("horizontalMovement", 0);
        } else if(moveDirection == Vector2.right)
        {
            animator.SetFloat("verticalMovement", 0);
            animator.SetFloat("horizontalMovement", 1);
        } else if(moveDirection == Vector2.left)
        {
            animator.SetFloat("verticalMovement", 0);
            animator.SetFloat("horizontalMovement", -1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        float radius = GetComponent<CircleCollider2D>().radius;

        RaycastHit2D raycastUp = Physics2D.Raycast(transform.position + Vector3.up * radius * 1.1f,  Vector3.up, 0.8f);
        RaycastHit2D raycastDown = Physics2D.Raycast(transform.position + Vector3.down * radius * 1.1f,  Vector3.down, 0.8f);
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position + Vector3.left * radius * 1.1f,  Vector3.left, 0.8f);
        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position + Vector3.right * radius * 1.1f,  Vector3.right, 0.8f);

        // Debug.DrawRay(transform.position + Vector3.up * radius * 1.1f, Vector2.up * 1.0f, Color.yellow);
        // Debug.DrawRay(transform.position + Vector3.down * radius * 1.1f, Vector2.down * 1.0f, Color.black);
        // Debug.DrawRay(transform.position + Vector3.left * radius * 1.1f, Vector2.left * 1.0f, Color.blue);
        // Debug.DrawRay(transform.position + Vector3.right * radius * 1.1f, Vector2.right * 1.0f, Color.red);

        List<Vector2> possibilites = new List<Vector2>();

        if(raycastUp.collider == null || raycastUp.collider.isTrigger || raycastUp.collider.tag == "Player" || raycastUp.collider.tag == "Enemy")
        {
            possibilites.Add(Vector2.up);
        }

        if (raycastDown.collider == null || raycastDown.collider.isTrigger || raycastDown.collider.tag == "Player" || raycastDown.collider.tag == "Enemy")
        {
            possibilites.Add(Vector2.down);
        }

        if (raycastLeft.collider == null || raycastLeft.collider.isTrigger || raycastLeft.collider.tag == "Player" || raycastLeft.collider.tag == "Enemy")
        {
            possibilites.Add(Vector2.left);
        }

        if (raycastRight.collider == null || raycastRight.collider.isTrigger || raycastRight.collider.tag == "Player" || raycastRight.collider.tag == "Enemy")
        {
            possibilites.Add(Vector2.right);
        }

        int randomIndex = Random.Range(0, possibilites.Count);

        moveDirection = possibilites[randomIndex];

        UpdateAnimation();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Explosion")
        {
            Destroy(gameObject);
        }
    }
}
