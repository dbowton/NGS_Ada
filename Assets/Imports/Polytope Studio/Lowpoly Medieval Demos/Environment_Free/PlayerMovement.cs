using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Camera cam;
    public GameObject character;

    public float speed = 5;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool attacking = false;

    void Update()
    {
        animator.SetBool("IsGrounded", isGrounded);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey("left shift"))
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !attacking)
        {
            animator.SetTrigger("Attack");
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        animator.SetFloat("Speed", move.magnitude * speed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            transform.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X");
            //cam.transform.localEulerAngles += character.transform.localPosition * -Input.GetAxis("Mouse Y")  ;
            cam.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y"));
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public void AttackStart()
    {
        attacking = true;
    }

    public void AttackEnd()
    {
        attacking = false;
    }

    public void FootL()
    {

    }

    public void FootR()
    {

    }
}