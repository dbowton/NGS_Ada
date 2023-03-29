using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimplePlayer : MonoBehaviour
{
	CharacterController controller;

	private void Awake()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		Vector3 movement = Vector3.zero;
		if (Input.GetKey(KeyCode.W)) movement += transform.forward;
		if (Input.GetKey(KeyCode.S)) movement -= transform.forward;
		if (Input.GetKey(KeyCode.D)) movement += transform.right;
		if (Input.GetKey(KeyCode.A)) movement -= transform.right;

		movement.Normalize();

		controller.SimpleMove(movement * 4);

		transform.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X");

		if (Input.GetKeyDown(KeyCode.Space)) Destroy(gameObject);
	}

	private void OnDestroy()
	{
		CameraGameManager.instance.player = null;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
