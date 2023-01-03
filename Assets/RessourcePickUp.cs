using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcePickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInParent<ScriptRessourceManager>().GainRessources("stone", 5);
            Destroy(this.gameObject);
        }
    }
}
