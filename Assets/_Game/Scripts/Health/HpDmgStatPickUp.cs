using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpDmgStatPickUp : MonoBehaviour
{
    #region Values
    public int amount = 5;
    public bool is_damage = false;
    public string type = "health";
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //health
            if(!is_damage)
                other.GetComponent<ScriptHealth>().GainStat(type, amount);
            //damage
            else
                other.GetComponent<ScriptHealth>().Suffer(type, amount);
            Destroy(this.gameObject);
        }
    }
}
