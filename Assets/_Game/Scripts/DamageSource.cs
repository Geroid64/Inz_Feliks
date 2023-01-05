using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    public int damage = 10;
    public string damage_type = "damage";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("OTRZYMUJE OBRAZENIA");
            other.GetComponent<ScriptHealth>().Suffer(damage_type, damage);
        }
    }
}
