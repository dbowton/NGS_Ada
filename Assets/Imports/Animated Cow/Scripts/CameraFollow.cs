using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class CameraFollow : MonoBehaviour
{
    public float cameraSpeed = 15;
    public float zOffset = 22;
    public float yOffset = 15;
    public bool smoothFollow = true;

    private Transform target;
    private Vector3 newPos;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(Tags.Cow).transform;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = transform.position;
        newPos.x = target.position.x;
        newPos.z = target.position.z - zOffset;
        newPos.y = target.position.y + yOffset;

        if (smoothFollow)
            transform.position = Vector3.Lerp(transform.position, newPos, cameraSpeed * Time.deltaTime);
        else
            transform.position = newPos;
    }
}
