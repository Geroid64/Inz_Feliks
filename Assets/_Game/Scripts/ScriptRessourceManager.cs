using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRessourceManager : MonoBehaviour
{
    #region Values
    public GameObject player;
    private GeneralFunctionsStats Gf_stats;

    [SerializeField] private UIDefault ui_default;

    int wood_amount, stone_amount, metal_amount, wool_amount;
    int money;
    public int max_amount, max_money;

    #endregion

    void Start()
    {
        Gf_stats = player.GetComponent<GeneralFunctionsStats>();
        stone_amount = 25;
    }

    #region Creating Ressources
    //NEED: save mechanic to create from save file
    #endregion

    #region Reading Ressources
    //NEED: UI element for values
    #endregion
    
    #region Updating Ressources
    public void GainRessources(string ressource, int amount)
    {
        switch (ressource)
        {
            case "money":
                money = Gf_stats.CheckMaxAmount(money, amount, max_money);
                break;
            case "wood":
                wood_amount = Gf_stats.CheckMaxAmount(wood_amount, amount, max_amount);
                break;
            case "stone":
                stone_amount = Gf_stats.CheckMaxAmount(stone_amount, amount, max_amount);
                break;
            case "metal":
                metal_amount = Gf_stats.CheckMaxAmount(metal_amount, amount, max_amount);
                break;
            case "wool":
                wool_amount = Gf_stats.CheckMaxAmount(wool_amount, amount, max_amount);
                break;
        }
    }
    #endregion

    #region Deleting Ressources
    public void DeleteRessources(string ressource, int amount)
    {
        switch (ressource)
        {
            case "money":
                money = Gf_stats.CheckMinAmount(money , amount);
                break;
            case "wood":
                wood_amount = Gf_stats.CheckMinAmount(wood_amount, amount);
                break;
            case "stone":
                stone_amount = Gf_stats.CheckMinAmount(stone_amount, amount);
                ui_default.UIUpdateLabel("Lstone", stone_amount);
                break;
            case "metal":
                metal_amount = Gf_stats.CheckMinAmount(metal_amount, amount);
                break;
            case "wool":
                wool_amount = Gf_stats.CheckMinAmount(wool_amount, amount);
                break;
        }
    }
    #endregion
}
