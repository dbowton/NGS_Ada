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
        gameObject.GetComponent<Health>().OnDeath.AddListener(() => AudioManager.instance.Play("PlayerDeath"));
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            //transform.localEulerAngles += Vector3.up * movement.cam.transform.eulerAngles.x;
            //cam.transform.localEulerAngles += character.transform.localPosition * -Input.GetAxis("Mouse Y");
            //movement.cam.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y"));
        }

        if (!Input.GetKey(KeyCode.LeftAlt))
		{
            transform.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X");
        }

        if (Input.GetKey(KeyCode.L))
        {
            gameObject.GetComponent<Health>().Damage(1);
        }
    }

    public void Attack(bool isAttacking)
    {
        weapon.isAttacking = isAttacking;
        AudioManager.instance.Play("SwordSwing");
        //AudioManager.instance.Play("Test");
    }
}
