using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteRoof : MonoBehaviour
{
    private int length;
    public string concerned_tag = "Inside";
    private List<GameObject> lista = new List<GameObject>();
    
    private void Start()
    {
        length = GameObject.FindGameObjectsWithTag(concerned_tag).Length;
        for (int i = 0; i < length; i++)
            lista.Add(GameObject.FindGameObjectsWithTag(concerned_tag)[i]);
    }

    private void OnTriggerStay(Collider collider)
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
