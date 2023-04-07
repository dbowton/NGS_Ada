using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    public PlayerMovement movement;
    public Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Health>().OnDeath.AddListener(() => movement.animator.SetTrigger("death"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            transform.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X");
            //cam.transform.localEulerAngles += character.transform.localPosition * -Input.GetAxis("Mouse Y")  ;
            movement.cam.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y"));
        }

        if (Input.GetKey(KeyCode.L))
        {
            gameObject.GetComponent<Health>().Damage(1);
        }
    }

    void Attack()
    {

    }
}
