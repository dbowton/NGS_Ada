using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CameraController : MonoBehaviour
{
	public float transitionTime = 0;
	public float weight = 0f;

	[HideInInspector] public Vector3 setPos = Vector3.zero;
	[HideInInspector] public Quaternion setRot = Quaternion.identity;
	[SerializeField] bool SetCameraTo = false;

	public UnityEvent onTransitioned = null;

	protected virtual void OnValidate()
	{
		if (SetCameraTo)
		{
			Camera.main.transform.SetPositionAndRotation(transform.position, transform.rotation);
			SetCameraTo = false;
		}
	}

	protected virtual void Awake()
	{
		setPos = transform.position;
	}

	protected virtual void Start()
	{
		if (SmartCamera.instance == null)
			Camera.main.gameObject.AddComponent<SmartCamera>();

		SmartCamera.instance.AddCamera(this);
	}

	protected virtual void OnDestroy()
	{
		SmartCamera.instance.RemoveCamera(this);
	}
}
