using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHiddenSeeThrough : MonoBehaviour
{
    public Camera cam;
    public GameObject sphere;
    public LayerMask mask;
    public string tagname = "PlayerSee";

    void FixedUpdate()
    {
        RaycastHit raycast;
        if (Physics.Raycast(cam.transform.position, (sphere.transform.position - cam.transform.position).normalized, out raycast, Mathf.Infinity, ~mask))
        {

            if (raycast.collider.gameObject.tag == tagname)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycast.distance, Color.green);
                Debug.Log("Did Hit: " + raycast.collider.gameObject.name);
                sphere.SetActive(false);

            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
                Debug.Log("Did not Hit");
                sphere.SetActive(true);
            }
        }
    }
}
