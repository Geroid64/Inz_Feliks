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
    Vector3 forward, right, down,screen_size;

    private void Start()
    {
        forward = cam.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        controller.detectCollisions = false;
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        down.y = -1;
        screen_size = new Vector3(Screen.width, Screen.height, 0);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool sprint = Input.GetKey(KeyCode.LeftShift);


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 iso_dir = new Vector3();

        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Quaternion rotation = Quaternion.LookRotation(point - transform.position);
            transform.rotation = rotation;
        }
        
        if (!controller.isGrounded)
            iso_dir = horizontal * right + vertical * forward + down;
        else
            iso_dir = horizontal * right + vertical * forward;

        if (iso_dir.magnitude >= 0.1f)
        {
            if(sprint==true)
                controller.Move(iso_dir * (speed+5) * Time.deltaTime);
            else
                controller.Move(iso_dir * speed * Time.deltaTime);
        }
    }


}