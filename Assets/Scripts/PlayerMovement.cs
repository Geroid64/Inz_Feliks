using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 6f;
    public float smooth = 0.1f;
    public float smoothVelocity;
    public bool isIso = true;
    public Camera camra;
    Vector3 forward, right,down;

    private void Start()
    {
        forward = camra.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        controller.detectCollisions = false;
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        down.y = -1; 
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 isodir = new Vector3();
        if (isIso)
        {
            Debug.Log(vertical);
            if (!controller.isGrounded)
                isodir = horizontal * right + vertical * forward + down;
            else
                isodir = horizontal * right + vertical * forward;
            if (isodir.magnitude >= 0.1f)
            {
                float angle = Mathf.Atan2(isodir.x, isodir.z) * Mathf.Rad2Deg;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVelocity, smooth);

                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
                if(sprint==true)
                    controller.Move(isodir * (speed+5) * Time.deltaTime);
                else
                    controller.Move(isodir * speed * Time.deltaTime);
            }
        }
        else
        {
            if (direction.magnitude >= 0.1f)
            {
                float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVelocity, smooth);

                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
                if (sprint == true)
                    controller.Move(direction * (speed+5) * Time.deltaTime);
                else
                    controller.Move(direction * speed * Time.deltaTime);
            }
        }
    }

}
