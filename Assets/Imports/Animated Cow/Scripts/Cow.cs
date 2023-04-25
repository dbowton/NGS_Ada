using UnityEngine;
using Extension;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(NavMeshAgent))]
public class Cow : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform center;
    public Vector3 extents;
    public float minDistanceToTarget = 0.5f;

    public float moveSpeed;
    public bool walking;
    public bool mooTrigger;
    public bool followTarget;
    public float followTurnSpeed = 0.05f;
    public Vector3 target;
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

    [SerializeField] Transform roamTransform;
    public bool atDestination;

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isMooing = false;
        agent = GetComponent<NavMeshAgent>();

		target.x = center.position.x + Random.Range(0, extents.x);
        target.y = transform.position.y;
		target.z = center.position.z + Random.Range(0, extents.z);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGroundStatus();

        if (isGrounded && walking)
            rigidbody.velocity = (transform.forward * moveSpeed);
        else if (rigidbody.useGravity)
            rigidbody.velocity = Physics.gravity;

        if (followTarget)
        {
			if (Vector3.Distance(transform.position, target) < moveSpeed)
            {
                target.x = center.position.x + Random.Range(-extents.x, extents.x);
    			target.y = transform.position.y;
                target.z = center.position.z + Random.Range(-extents.z, extents.z);
    
            }
            agent.destination = target;

            followOffset = target - transform.position;
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

        if (Vector3.Distance(this.transform.position, roamTransform.position) <= 1.5)
        {
            StartMoo();
            //DoRoam();
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

    public void DoRoam()
    {
        walking = true;
		this.transform.rotation = Quaternion.AngleAxis(Random.Range(-90, 90), Vector3.up);
		Vector3 forward = this.transform.rotation * transform.forward;
		target.transform.position = roamTransform.position + forward * Random.Range(5f, 10f);
	}
}
