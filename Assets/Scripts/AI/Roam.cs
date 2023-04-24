using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roam : MonoBehaviour
{
    [SerializeField] Transform roamTransform;
    public bool atDestination;
    Vector3 destination;

    void Start()
    {
        
    }

    public void DoRoam()
	{
        this.transform.rotation = Quaternion.AngleAxis(Random.Range(-90, 90), Vector3.up);
        Vector3 forward = this.transform.rotation * transform.forward;
        destination = roamTransform.position + forward * Random.Range(3f, 3f);
    }

    void Update()
    {
        if (Vector3.Distance(this.transform.position, destination) <= 1.5)
        {
            DoRoam();
        }
    }
}
