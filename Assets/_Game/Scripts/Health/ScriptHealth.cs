using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ScriptHealth : MonoBehaviour
{
    #region Values
    public GameObject actor;
    private GeneralFunctionsStats Gf_stats;
    public PlayerLostScript lost_script;
    //public UIDefault ui_default;
    public UIDocument ui_player;
    public MainPlayerHUD player_hud;
    VisualElement[] health_segments = new VisualElement[10];
    public int amount_of_segments = 10;
    public int segment_index=0;
    
    public int hp = 50;
    public int max_hp = 100;
    public bool is_player = false;

    string main_UI_health_name = "LifeSegment";
    #endregion


    void Start()
    {
        Gf_stats = actor.GetComponent<GeneralFunctionsStats>();
        if (is_player)
        {
            for (int i = 0; i < amount_of_segments; i++)
            {
                health_segments[i] = ui_player.rootVisualElement.Q(main_UI_health_name + i.ToString()) as VisualElement;
                health_segments[i].SetEnabled(false);
            }
            UpdateHealthUI(true);
        }


    }

    #region Health
    public void TakeDamage(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            Debug.Log("+++++++++++++++++took"+ damage +"dmg");
            if (is_player)
            {

                UpdateHealthUI(false);
            }
            if (hp<=0)
            {
                Died();
            }
        }
        else
            Died();
    }

    public void Died()
    {
        if (is_player==true)
        {
            lost_script.LoserManager();
        }
        if(is_player==false)
            Destroy(this.gameObject);
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
                if (is_player)
                {
                    UpdateHealthUI(true);
                }
                break;
            case "speed":
                PlayerMovement.speed += amount;
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
                PlayerMovement.speed -= amount;
                break;
            case "break":
                TakeDamage(amount);
                PlayerMovement.speed -= amount/2;
                break;
        }
    }

    public void UpdateHealthUI(bool heal)
    {

        if (player_hud.is_player_hud)
        {
            if (heal)
            {
                for (int i = 0; i < hp / amount_of_segments; i++)
                {
                    health_segments[i].SetEnabled(true);
                }
            }
            else
            {
                for (int i = amount_of_segments - 1; i >= hp / amount_of_segments; i--)
                {
                    health_segments[i].SetEnabled(false);
                }
            }
        }

    }
    #endregion
}
