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
    public LayerMask walkableMask;

    Vector3 velocity;
    bool isGrounded;
    bool canAttack = true;
    int combinedLayerMask;
    int attackCount = 0;

    Timer attackTimer;


    private void Start()
    {
        combinedLayerMask = groundMask | walkableMask;
        attackTimer = new Timer(2.0f, () => attackCount = 0);
        attackTimer.End();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            DebugPause();
        }

        //if (attackTimer.IsOver) attackCount = 0;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //animator.SetBool("Jump", (Input.GetKeyDown(KeyCode.Space) && isGrounded));

    }

    private void FixedUpdate()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, combinedLayerMask);
        //isGrounded = controller.isGrounded;
        animator.SetBool("IsGrounded", isGrounded);

       

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        if (Input.GetKey("left shift"))
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }

        /*if (Input.GetKeyDown(KeyCode.Mouse0) && !canAttack)
        {
            animator.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackCount % 2 == 1 && !canAttack)
        {
            animator.SetTrigger("Attack 2");
        }*/

        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            switch (attackCount % 2)
            {
                case 0:
                    animator.SetTrigger("Attack");
                    break;
                case 1:
                    animator.SetTrigger("Attack 2");
                    break;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        animator.SetFloat("Speed", move.magnitude * speed);

        

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void AttackStart()
    {
        //Debug.Log("Attack start");
        gameObject.GetComponent<playerInput>().Attack(true);
    }

    public void AttackEnd()
    {
        //Debug.Log("Attack end");
        gameObject.GetComponent<playerInput>().Attack(false);
        canAttack = true;

    }

    public void AttackAnimationStart()
    {
        //Debug.Log("Attack anim start");
        canAttack = false;
        attackCount++;
        attackTimer.Reset();
    }

    public void FootL()
    {

    }

    public void FootR()
    {

    }

    public void DebugPause()
    {
        if (true)
        {
            return;
        }

    }

}