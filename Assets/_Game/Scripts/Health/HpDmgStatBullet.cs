using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpDmgStatBullet : MonoBehaviour
{
    #region Values
    public int amount = 5;
    public bool is_damage = false;
    public string type = "health";
    public string tag_to_find = "Player";
    public LayerMask layers;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6)
        {
            if (other.gameObject.CompareTag(tag_to_find))
            {
                //health
                if (!is_damage)
                    other.GetComponent<ScriptHealth>().GainStat(type, amount);
                //damage
                else
                    other.GetComponent<ScriptHealth>().Suffer(type, amount);

            }
            Destroy(this.gameObject);
        }
    }
}
