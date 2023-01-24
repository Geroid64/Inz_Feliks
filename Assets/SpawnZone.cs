using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public GameObject[] list_to_spawn;


    void Start()
    {
        Instantiate(list_to_spawn[0], transform.position, Quaternion.identity);
        Destroy(this);
    }
}
