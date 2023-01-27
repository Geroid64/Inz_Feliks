using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptResourceManager : MonoBehaviour
{
    #region Values
    public GameObject player;
    private GeneralFunctionsStats Gf_stats;

    [SerializeField] private UIDefault ui_default;
    [SerializeField] private MainPlayerHUD ui_player;

    int wood_amount, stone_amount, metal_amount, wool_amount;
    int money;
    public int ammo_amount;
    public int max_amount, max_money, max_ammo;

    #endregion

    private void Awake()
    {
        //stone_amount = 25;
        ammo_amount = 60;
        money = 0;
        stone_amount = 0;
        wood_amount = 0;
        metal_amount = 0;


        ui_player.UIUpdateLabel("AmmoLabel", "", ammo_amount);
        ui_player.UIUpdateLabel("MoneyAmountLabel", "Money: ", money);
        ui_player.UIUpdateLabel("WoodAmountLabel", "Wood: ", wood_amount);
        ui_player.UIUpdateLabel("StoneAmountLabel", "Stone: ", stone_amount);
        ui_player.UIUpdateLabel("MetalAmountLabel", "Metal: ", metal_amount);


        Gf_stats = player.GetComponent<GeneralFunctionsStats>();
    }

    void Start()
    {

    }

    #region Creating Resources
    //NEED: save mechanic to create from save file
    #endregion

    #region Reading Resources
    //NEED: save file
    #endregion

    #region Updating Resources
    public void GainResources(string resource, int amount)
    {
        switch (resource)
        {
            case "ammo":
                ammo_amount = Gf_stats.CheckMaxAmount(ammo_amount, amount, max_ammo);
                ui_player.UIUpdateLabel("AmmoLabel", "", ammo_amount);
                break;
            case "money":
                money = Gf_stats.CheckMaxAmount(money, amount, max_money);
                ui_player.UIUpdateLabel("MoneyAmountLabel", "Money: ", money);
                break;
            case "wood":
                wood_amount = Gf_stats.CheckMaxAmount(wood_amount, amount, max_amount);
                ui_player.UIUpdateLabel("WoodAmountLabel", "Wood: ", wood_amount);
                break;
            case "stone":
                stone_amount = Gf_stats.CheckMaxAmount(stone_amount, amount, max_amount);
                ui_player.UIUpdateLabel("StoneAmountLabel", "Stone: ", stone_amount);
                break;
            case "metal":
                metal_amount = Gf_stats.CheckMaxAmount(metal_amount, amount, max_amount);
                ui_player.UIUpdateLabel("MetalAmountLabel", "Metal: ", metal_amount);
                break;
            case "wool":
                wool_amount = Gf_stats.CheckMaxAmount(wool_amount, amount, max_amount);
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
                ui_player.UIUpdateLabel("AmmoLabel", "", ammo_amount);
                break;
            case "money":
                money = Gf_stats.CheckMinAmount(money , amount);
                ui_player.UIUpdateLabel("MoneyAmountLabel", "Money: ", money);
                break;
            case "wood":
                wood_amount = Gf_stats.CheckMinAmount(wood_amount, amount);
                ui_player.UIUpdateLabel("WoodAmountLabel", "Wood: ", wood_amount);
                break;
            case "stone":
                stone_amount = Gf_stats.CheckMinAmount(stone_amount, amount);
                ui_player.UIUpdateLabel("StoneAmountLabel", "Stone: ", stone_amount);
                break;
            case "metal":
                metal_amount = Gf_stats.CheckMinAmount(metal_amount, amount);
                ui_player.UIUpdateLabel("MetalAmountLabel", "Metal: ", metal_amount);
                break;
            case "wool":
                wool_amount = Gf_stats.CheckMinAmount(wool_amount, amount);
                break;
        }
    }
    #endregion
}
