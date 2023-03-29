using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : CameraController
{
    void Update()
    {
        setPos = transform.position;
        setRot = transform.rotation;
    }
}
