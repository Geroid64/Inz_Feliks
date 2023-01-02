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
    public int dmg = 20;
    #endregion

    void Start()
    {
        Gf_stats = player.GetComponent<GeneralFunctionsStats>();
        ui_default.UIUpdateLabel("Llife", hp);
    }

    #region Health
    public void TakeDamage(int damage)
    {
        if (hp <= 0)
            Died();
        else
        {
            hp -= dmg;
            Debug.Log("+++++++++++++++++took"+ dmg +"dmg");
            ui_default.UIUpdateLabel("Llife",hp);
        }
    }

    public void Died()
    {
        Debug.Log("===============died");
    }
    #endregion

    #region General
    public void GainStat(string stat, int amount)
    {
        switch (stat)
        {
            case "health":
                hp = Gf_stats.CheckMaxAmount(hp, amount,max_hp);
                ui_default.UIUpdateLabel("Llife",hp);
                break;
            case "speed":
                GetComponent<PlayerMovement>().speed += amount;
                break;
            case "damage":
                TakeDamage(amount);
                break;
            case "slow":
                GetComponent<PlayerMovement>().speed -= amount;
                break;
        }
    }
    #endregion
}
