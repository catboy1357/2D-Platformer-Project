using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Rigidbody2D rb;
    public Collider2D ignoredCollider;

    public float runSpeed = 40f;
    public float fallThreshold = 2;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed_X", Mathf.Abs(horizontalMove));
        animator.SetFloat("Speed_Y", rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    // Update the jump event
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    // Checks if player leaves the ground without jumping
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && ignoredCollider != collision.collider)
        {
            if (rb.velocity.y < fallThreshold)
            {
                animator.SetBool("IsFalling", true);
            }
        }
        {
            animator.SetBool("IsFalling", true);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("IsFalling", false);
        }
    }

    // move the player
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
