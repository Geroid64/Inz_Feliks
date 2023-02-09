using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public static bool can_move = true;
    public static float speed = 6f;
    public float smooth = 0.1f;
    public float smooth_velocity;
    public Camera cam;
    Vector3 z_axis, x_axis, gravity;

    private void Start()
    {
        z_axis = cam.transform.forward;
        z_axis.y = 0;
        controller.detectCollisions = false;
        z_axis = Vector3.Normalize(z_axis);
        x_axis = Quaternion.Euler(new Vector3(0, 90, 0)) * cam.transform.forward;
        gravity.y = -1;
    }

    void FixedUpdate()
    {
        if (can_move)
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
            {
                Debug.Log("nie dotyka ziemi");
                iso_dir = horizontal * x_axis + vertical * z_axis + gravity;
            }
            else
                iso_dir = horizontal * x_axis + vertical * z_axis;

            if (iso_dir.magnitude >= 0.1f)
            {
                if (sprint == true)
                    controller.Move(iso_dir * (speed + 5) * Time.deltaTime);
                else
                    controller.Move(iso_dir * speed * Time.deltaTime);
            }
        }

    }


}