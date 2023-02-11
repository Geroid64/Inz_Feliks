using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    
    public bool far = false;

    public Camera cam;
    private Vector3 camera_player_offset;
    public float smoothing = 0.1f;
    public Vector3 mouse_location, screen_size, new_position;
    public float camera_sensitivity = 0.05f;
    public float smooth = 0.1f;
    public float border = 1;
    
    void Start()
    {
        screen_size = new Vector3(Screen.width, Screen.height,0);
        camera_player_offset = transform.position - player.transform.position;
    }

    void Update()
    {
        mouse_location = Input.mousePosition;

        mouse_location = (mouse_location - screen_size * 0.5f) * camera_sensitivity;
        mouse_location = new Vector3(Mathf.Clamp(mouse_location.x, -border,border), Mathf.Clamp(mouse_location.y, -border, border));

        new_position = Quaternion.Euler(new Vector3(0,45,0)) * mouse_location;
            
        new_position += camera_player_offset + player.transform.position;

        Debug.Log(new_position);
        new_position = Vector3.Lerp(transform.position, new_position, smoothing);
        transform.position = new_position;
    }

}