using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    #region Values
    public int health_amount = 5;
    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<ScriptHealth>().GainStat("health", health_amount);
            Destroy(this.gameObject);
        }
    }
}
