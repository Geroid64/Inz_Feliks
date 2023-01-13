using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    
    public bool far = false;

    public Camera cam;
    private Vector3 Offset;
    public float CamSmooth = 0.1f;
    public Vector3 mouselocation, screenSize, newPos;
    public float camsens = 0.05f;
    public float smooth = 0.1f;
    public float border = 1;
    
    void Start()
    {
        screenSize = new Vector3(Screen.width, Screen.height,0);
        Offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        mouselocation = Input.mousePosition;

        mouselocation = (mouselocation - screenSize * 0.5f) * camsens;
        mouselocation = new Vector3(Mathf.Clamp(mouselocation.x, -border,border), Mathf.Clamp(mouselocation.y, -border, border));

        newPos = Quaternion.Euler(new Vector3(0,45,0)) * mouselocation;
            
        newPos += Offset + player.transform.position;

        Debug.Log(newPos);
        newPos = Vector3.Lerp(transform.position, newPos, CamSmooth);
        transform.position = newPos;
    }

}