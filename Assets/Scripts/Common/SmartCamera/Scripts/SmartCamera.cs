using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCamera : MonoBehaviour
{
	public static SmartCamera instance = null;
	public Camera main { get { return GetComponent<Camera>(); } }
	public CameraController active;

	private List<CameraController> controllers = new List<CameraController>();

	private bool transitioning = false;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(this);
	}

	float transitionTimer = 0;
	private void Update()
	{
		if (active == null && !CheckWeights()) return;

		Vector3 targetPosition = active.setPos;
		Quaternion targetRotation = active.setRot;
		if (transitioning)
		{
			transitionTimer += Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, targetPosition, Mathf.Min(transitionTimer / active.transitionTime, 1));
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Mathf.Min(transitionTimer / active.transitionTime, 1));

			transitioning = transitionTimer < active.transitionTime && Vector3.Distance(transform.position, targetPosition) > 0.05f && Quaternion.Angle(transform.rotation, targetRotation) > 2.5f;

			if (!transitioning)
				active.onTransitioned.Invoke();
		}
		else
		{
			transform.position = targetPosition;
			transform.rotation = targetRotation;
		}
	}

	public void AddCamera(CameraController camera)
	{
		if (controllers.Contains(camera)) return;
		controllers.Add(camera);
		CheckWeights();
	}

	public void RemoveCamera(CameraController camera) 
	{ 
		controllers.Remove(camera);
		CheckWeights();
	}

	private bool CheckWeights()
	{
		if (controllers.Count == 0) return false;

		CameraController controller = controllers[0];
		for(int i = 0; i < controllers.Count; i++) 
			if (controllers[i].weight > controller.weight) 
				controller = controllers[i];

		if (controller != active)
			Transition(controller);

		return true;
	}

	private void Transition(CameraController newCam)
	{
		transitioning = newCam.transitionTime > 0f;
		transitionTimer = 0f;
		active = newCam;
	}
}
