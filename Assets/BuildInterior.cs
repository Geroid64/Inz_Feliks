using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildInterior : MonoBehaviour
{
    public Camera cam;
    public GameObject to_spawn;
    public ScriptResourceManager resources;
    public ScriptHealth player_health;

    public string fund_name;
    //0,wood|1,stone|2,metal|3,money
    public int[] cost;

    public bool gives_buff;
    public bool is_res;
    public string buff_type;
    public int buff_amount;
    
    bool open,in_collider = false;

    public UIDocument ui_build;
    public MainPlayerHUD ui_player;
    public Button buy_button;
    public Label fund_label, player_resource_label;


    private void Awake()
    {
        if (!AllProjectsManager.funded_list.ContainsKey(fund_name))
        {
            AllProjectsManager.funded_list.Add(fund_name, false);
        }
            

        fund_label = ui_build.rootVisualElement.Q("DialogInfoLabel") as Label;
        player_resource_label = ui_build.rootVisualElement.Q("PlayerResources") as Label;
        buy_button = ui_build.rootVisualElement.Q("BuyButton") as Button;
        buy_button.clicked += () => CheckProgress();
        to_spawn.SetActive(AllProjectsManager.funded_list[fund_name]);
        ui_build.rootVisualElement.style.display = DisplayStyle.None;
    }
    
    private void Update()
    {
        if (in_collider)
        {
            if (Input.GetButtonDown("Interact"))
            {
                open = !open;
                if (open)
                {
                    UpdatePlayerResourceUI();
                    UpdateFundingUI();
                    ui_build.rootVisualElement.style.display = DisplayStyle.Flex;
                    ui_player.DisablePlayerUI(true);
                }
                else
                {
                    ui_build.rootVisualElement.style.display = DisplayStyle.None;
                    ui_player.DisablePlayerUI(false);
                }
            }
        }
    }

    private void Start()
    {
        if (AllProjectsManager.funded_list[fund_name])
        {
            to_spawn.SetActive(AllProjectsManager.funded_list[fund_name]);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            in_collider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ui_build.rootVisualElement.style.display = DisplayStyle.None;
        open = false;
        ui_player.DisablePlayerUI(false);
        in_collider = false;
    }

    public void CheckProgress()
    {
        if (
            is_enough(ScriptResourceManager.wood_amount, 0)&&
            is_enough(ScriptResourceManager.stone_amount, 1)&&
            is_enough(ScriptResourceManager.metal_amount, 2)&&
            is_enough(ScriptResourceManager.money, 3)
            )
        {
            resources.DeleteResources("wood", cost[0]);
            resources.DeleteResources("stone", cost[1]);
            resources.DeleteResources("metal", cost[2]);
            resources.DeleteResources("money", cost[3]);
            if (gives_buff)
            {
                GiveBuff(is_res,buff_type,buff_amount);
            }

            AllProjectsManager.funded_list[fund_name] = true;
            to_spawn.SetActive(true);
            ui_player.DisablePlayerUI(false);
            Destroy(gameObject);
        }
        else
            fund_label.text = "Not enough funds, sorry";
    }

    public bool is_enough(int amount, int index)
    {
        if (amount >= cost[index])
            return true;
        else
            return false;
    }

    public void UpdatePlayerResourceUI()
    {
        player_resource_label.text = "wood: " + ScriptResourceManager.wood_amount.ToString() + "\n";
        player_resource_label.text += "stone: " + ScriptResourceManager.stone_amount.ToString() + "\n";
        player_resource_label.text += "metal: " + ScriptResourceManager.metal_amount.ToString() + "\n";
        player_resource_label.text += "money: " + ScriptResourceManager.money.ToString();
    }

    public void UpdateFundingUI()
    {
        fund_label.text = "To create this You will need: " + "\n";
        if (cost[0]>0)
            fund_label.text += "wood: " + cost[0].ToString() + "\n";
        if (cost[1] > 0)
            fund_label.text += "stone: " + cost[1].ToString() + "\n";
        if (cost[2] > 0)
            fund_label.text += "metal: " + cost[2].ToString() + "\n";
        if (cost[3] > 0)
            fund_label.text += "money: " + cost[3].ToString() + "\n";
        fund_label.text += "It can help you with ";
    }

    public void GiveBuff(bool res, string type, int amount)
    {
        if (res)
        {
            switch (type)
            {
                case "wood":
                    ScriptResourceManager.max_wood += amount;
                    break;
                case "stone":
                    ScriptResourceManager.max_wood += amount;
                    break;
                case "metal":
                    ScriptResourceManager.max_wood += amount;
                    break;
                case "ammo":
                    ScriptResourceManager.max_ammo += amount;
                    break;
            }
        }
        else
            player_health.GainStat(type, amount);
    }
}
