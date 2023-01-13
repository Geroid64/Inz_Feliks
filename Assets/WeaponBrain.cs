using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBrain : MonoBehaviour
{
    public Camera cam;
    public Transform barrel;
    public GameObject idk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = transform.position;
            
            
            GameObject bullet = Instantiate(idk, barrel.position, barrel.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(pos*4, ForceMode.Force);
            Debug.Log("Odpal");
        }
    }
}
