using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player; 
    public Vector3 Offset;
    public float CamSmooth = 0.1f;
    private Vector3 mouselocation, screenSize, newPos;
    public float camsens=0.05f;
    public float smooth = 0.1f;
    public float border = 1;
    Vector3 forward, right;
    void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height,0);
        Offset = transform.position - player.transform.position;

        forward = transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

    }


    void LateUpdate()
    {
        mouselocation = Input.mousePosition;

        mouselocation = (mouselocation - screenSize * 0.5f) * camsens;
        mouselocation = new Vector3(Mathf.Clamp(mouselocation.x, -border,border), Mathf.Clamp(mouselocation.y, -border, border));

        newPos = mouselocation;
        newPos = Quaternion.Euler(new Vector3(0,45,0))*newPos;
        newPos += Offset + player.transform.position;
        Debug.Log(newPos);
        newPos = Vector3.Lerp(transform.position, newPos, CamSmooth);
        transform.position = newPos;

        //direction = mouselocation.x * right + mouselocation.y * forward;
        //float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref smoothVelocity, smooth);
    }
}
