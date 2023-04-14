using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Cow : MonoBehaviour
{
    public float moveSpeed;
    public bool walking;
    public bool mooTrigger;
    public bool followTarget;
    public float followTurnSpeed = 0.05f;
    public Transform target;
    [Range(0f, 1f)]
    public float ChewAmount;
    [Range(0f, 1f)]
    public float TailSwipeAmount;
    [Range(-1f, 1f)]
    public float TurnAmount;
    public AudioSource mooAudioSource;
    public float groundCheckDistance;

    private Vector3 groundNormal;
    private bool isGrounded;
    private Rigidbody rigidbody;
    private Animator animator;
    private Vector3 followOffset;
    private Quaternion followRotation;
    private bool isMooing;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isMooing = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGroundStatus();

        if (isGrounded && walking)
            rigidbody.velocity = (transform.forward * moveSpeed);
        else if (rigidbody.useGravity)
            rigidbody.velocity = Physics.gravity;

        if (followTarget && target != null)
        {
            followOffset = target.position - transform.position;
            followRotation = Quaternion.LookRotation(followOffset);
            transform.rotation = Quaternion.Lerp(transform.rotation, followRotation, followTurnSpeed);
        }
        else if (walking)
        {
            transform.Rotate(transform.up, TurnAmount);
        }
    }

    private void Update()
    {
        if (!isMooing)
            animator.SetLayerWeight(LayerIndex.Head, ChewAmount);
        animator.SetLayerWeight(LayerIndex.Tail, TailSwipeAmount);
        animator.SetBool(Parameters.Walking, walking);

        if (mooTrigger)
        {
            animator.SetLayerWeight(LayerIndex.Head, 1f);
            animator.SetTrigger(Parameters.Moo);
            mooTrigger = false;
            mooAudioSource.Play();
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif

        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
        {
            groundNormal = hitInfo.normal;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            groundNormal = Vector3.up;
        }
    }

    public void StartMoo()
    {
        isMooing = true;
    }

    public void EndMoo()
    {
        isMooing = false;
        animator.SetLayerWeight(LayerIndex.Head, ChewAmount);
    }
}
