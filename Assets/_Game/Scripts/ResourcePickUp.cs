using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInParent<ScriptResourceManager>().GainResources("stone", 5);
            Destroy(this.gameObject);
        }
    }
}
