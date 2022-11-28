using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteRoof : MonoBehaviour
{
    private int length;
    private List<GameObject> lista = new List<GameObject>();
    
    private void Start()
    {
        length = GameObject.FindGameObjectsWithTag("Inside").Length;
        for (int i = 0; i < length; i++)
            lista.Add(GameObject.FindGameObjectsWithTag("Inside")[i]);
    }

    private void OnTriggerEnter(Collider collider)
    {
        print(collider.tag);

        if (collider.tag == "Player")
        { 
            for (int i = 0; i < length; i++)
                lista[i].GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            for (int i = 0; i < length; i++)
                lista[i].GetComponent<Renderer>().enabled = true;
        }
    }
}
