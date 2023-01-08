using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickUp : MonoBehaviour
{
    #region Values
    public int resource_amount = 5;
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInParent<ScriptResourceManager>().GainResources("stone", resource_amount);
            Destroy(this.gameObject);
        }
    }
}
