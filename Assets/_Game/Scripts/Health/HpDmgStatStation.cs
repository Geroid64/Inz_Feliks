using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpDmgStatStation : MonoBehaviour
{
    #region Values
    public bool is_damage = false;
    public float time = 0.3f;
    public int station_life = 5;
    public int type_amount = 5;
    public string type = "health";
    
    ScriptHealth player;
    Coroutine routine = null;
    #endregion

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.GetComponent<ScriptHealth>();
            routine = StartCoroutine(Heal(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (routine != null)
                StopCoroutine(routine);
        }
    }

    IEnumerator Heal(ScriptHealth player_hp)
    {

        for (int i = station_life; i > 0; i--)
        {
            Debug.Log("++++++++++++");
            if (!is_damage)
            {
                player_hp.GainStat(type, type_amount);
            }
            else
            {
                player_hp.Suffer(type, type_amount);
            }
            
            station_life--;
            yield return new WaitForSeconds(time);
        }
    }
}
