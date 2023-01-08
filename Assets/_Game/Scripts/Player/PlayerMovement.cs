using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float smooth = 0.1f;
    public float smooth_velocity;
    public Camera cam;
    Vector3 forward, right, down;

    private void Start()
    {
        forward = cam.transform.forward;
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
        Vector3 iso_dir = new Vector3();

        if (!controller.isGrounded)
            iso_dir = horizontal * right + vertical * forward + down;
        else
            iso_dir = horizontal * right + vertical * forward;

        if (iso_dir.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(iso_dir.x, iso_dir.z) * Mathf.Rad2Deg;
            float smooth_angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smooth_velocity, smooth);

            transform.rotation = Quaternion.Euler(0f, smooth_angle, 0f);
            if(sprint==true)
                controller.Move(iso_dir * (speed+5) * Time.deltaTime);
            else
                controller.Move(iso_dir * speed * Time.deltaTime);
        }
    }
}
