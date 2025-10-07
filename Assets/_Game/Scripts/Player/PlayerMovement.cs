using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public static float speed = 11f;
    public float stepson;
    public Camera cam;

        float gravity;


    private void Start()
    {
        gravity = -0.5f;
    }

    void Update()
    {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            bool sprint = Input.GetKey(KeyCode.LeftShift);


            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            Vector3 iso_dir;

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
                    iso_dir = new Vector3(horizontal, gravity, vertical);
            else
                iso_dir = new Vector3(horizontal ,0 ,vertical);
            iso_dir = Quaternion.Euler(new Vector3(0, 45, 0)) * iso_dir;
            if (iso_dir.magnitude >= stepson)
            {
                if (sprint == true)
                    controller.Move(iso_dir * (speed + 2) * Time.deltaTime);
                else
                    controller.Move(iso_dir * speed * Time.deltaTime);
            }
    }


}