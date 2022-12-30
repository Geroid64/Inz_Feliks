using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScriptHealth : MonoBehaviour
{
    #region Values
    public UIDefault ui_default;
    public int hp = 100;
    public int dmg = 20;
    #endregion

    void Start()
    {
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
    public void GainBuff(string stat, int amount)
    {
        switch (stat)
        {
            case "health":
                hp += amount;
                ui_default.UIUpdateLabel("Llife",hp);
                break;
            case "speed":
                GetComponent<PlayerMovement>().speed += amount;
                break;
        }
    }
    #endregion
}
