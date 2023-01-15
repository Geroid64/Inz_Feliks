using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScriptHealth : MonoBehaviour
{
    #region Values
    public GameObject player;
    private GeneralFunctionsStats Gf_stats;

    [SerializeField] private UIDefault ui_default;

    public int hp = 50;
    public int max_hp = 100;
    public bool is_plugged_to_ui = false;
    #endregion

    void Start()
    {
        Gf_stats = player.GetComponent<GeneralFunctionsStats>();
        if (ui_default!=null)
        {
            ui_default.UIUpdateLabel("Llife", hp);
        }
        
    }

    #region Health
    public void TakeDamage(int damage)
    {
        if (hp <= 0)
            Died();
        else
        {
            hp -= damage;
            Debug.Log("+++++++++++++++++took"+ damage +"dmg");
            if (ui_default != null)
            {
                ui_default.UIUpdateLabel("Llife", hp);
            }
        }
    }

    public void Died()
    {
        Debug.Log("===============died");
        Destroy(this.gameObject);
    }
    #endregion

    #region General
    public void GainStat(string stat, int amount)
    {
        switch (stat)
        {
            case "health":
                hp = Gf_stats.CheckMaxAmount(hp, amount,max_hp);
                if (ui_default != null)
                {
                    ui_default.UIUpdateLabel("Llife", hp);
                }
                break;
            case "speed":
                GetComponent<PlayerMovement>().speed += amount;
                break;
        }
    }

    public void Suffer(string stat, int amount)
    {
        switch (stat)
        {
            case "damage":
                TakeDamage(amount);
                break;
            case "slow":
                GetComponent<PlayerMovement>().speed -= amount;
                break;
            case "break":
                TakeDamage(amount);
                GetComponent<PlayerMovement>().speed -= amount/2;
                break;
        }
    }
    #endregion
}
