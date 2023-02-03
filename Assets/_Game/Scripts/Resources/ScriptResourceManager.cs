using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptResourceManager : MonoBehaviour
{
    #region Values

    public GameObject player;
    private GeneralFunctionsStats Gf_stats;
    public MainPlayerHUD ui_player;

    public static int wood_amount, stone_amount, metal_amount, wool_amount = 0;
    public static int money;
    public int max_money = 1000;
    public int ammo_amount;
    public static int max_wood = 70;
    public static int max_stone = 70;
    public static int max_metal = 70;
    public static int max_ammo = 120;
    

    #endregion

    private void Awake()
    {
        ammo_amount = max_ammo;
        if(ui_player.is_player_hud)
        {
            ui_player.UIUpdateLabel("AmmoLabel", "", ammo_amount);
            ui_player.UIUpdateLabel("MoneyAmountLabel", "Money: ", money);
            ui_player.UIUpdateLabel("WoodAmountLabel", "Wood: ", wood_amount);
            ui_player.UIUpdateLabel("StoneAmountLabel", "Stone: ", stone_amount);
            ui_player.UIUpdateLabel("MetalAmountLabel", "Metal: ", metal_amount);
        }

        Gf_stats = player.GetComponent<GeneralFunctionsStats>();
    }

    #region Updating Resources
    public void GainResources(string resource, int amount)
    {
        switch (resource)
        {
            case "ammo":
                ammo_amount = Gf_stats.CheckMaxAmount(ammo_amount, amount, max_ammo);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("AmmoLabel", "", ammo_amount);
                }
                break;
            case "money":
                money = Gf_stats.CheckMaxAmount(money, amount, max_money);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("MoneyAmountLabel", "Money: ", money);
                }
                break;
            case "wood":
                wood_amount = Gf_stats.CheckMaxAmount(wood_amount, amount, max_wood);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("WoodAmountLabel", "Wood: ", wood_amount);
                }
                break;
            case "stone":
                stone_amount = Gf_stats.CheckMaxAmount(stone_amount, amount, max_stone);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("StoneAmountLabel", "Stone: ", stone_amount);
                }
                break;
            case "metal":
                metal_amount = Gf_stats.CheckMaxAmount(metal_amount, amount, max_metal);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("MetalAmountLabel", "Metal: ", metal_amount);
                }
                break;
        }
    }
    #endregion

    #region Deleting Resources
    public void DeleteResources(string resource, int amount)
    {
        switch (resource)
        {
            case "ammo":
                ammo_amount = Gf_stats.CheckMinAmount(ammo_amount, amount);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("AmmoLabel", "", ammo_amount);
                }
                break;
            case "money":
                money = Gf_stats.CheckMinAmount(money , amount);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("MoneyAmountLabel", "Money: ", money);
                }
                break;
            case "wood":
                wood_amount = Gf_stats.CheckMinAmount(wood_amount, amount);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("WoodAmountLabel", "Wood: ", wood_amount);
                }
                break;
            case "stone":
                stone_amount = Gf_stats.CheckMinAmount(stone_amount, amount);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("StoneAmountLabel", "Stone: ", stone_amount);
                }
                break;
            case "metal":
                metal_amount = Gf_stats.CheckMinAmount(metal_amount, amount);
                if (ui_player.is_player_hud)
                {
                    ui_player.UIUpdateLabel("MetalAmountLabel", "Metal: ", metal_amount);
                }
                break;
        }
    }
    #endregion
}
