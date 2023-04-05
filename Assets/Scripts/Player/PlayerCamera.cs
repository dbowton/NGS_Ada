using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public float distance = 3;
    public float height = 2;
    public float shoulderOffset = 2;
    public bool switchShoulder;
    public float smoothTime = 0.25f;
    Vector3 lookTarget;
    Vector3 lookTargetVelocity;
    Vector3 currentVelocity;
    void LateUpdate()
    {
        Vector3 target = player.position + (-player.transform.forward * distance);
        Vector3 verticalPosition = Vector3.up * height;
        Vector3 shoulderPosition = switchShoulder ? transform.right * -shoulderOffset : transform.right * shoulderOffset;
        transform.position = Vector3.SmoothDamp(transform.position, target + shoulderPosition + verticalPosition, ref currentVelocity, smoothTime);
        lookTarget = Vector3.SmoothDamp(lookTarget, player.position + verticalPosition + shoulderPosition, ref lookTargetVelocity, smoothTime);
        transform.LookAt(lookTarget);
    }
}
