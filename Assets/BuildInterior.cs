using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildInterior : MonoBehaviour
{
    public Camera cam;
    public GameObject to_spawn;
    public ScriptResourceManager resources;

    public static bool funded = false;
    public bool open,in_collider = false;
    //bool check = false;
    public int index = 0;
    //0,wood|1,stone|2,metal|3,money
    public int[] cost;
    public string[] types;

    public UIDocument ui_build;
    public MainPlayerHUD ui_player;
    public Button buy_button;
    public Label fund_label;
    //GameObject project;


    private void Awake()
    {
        fund_label = ui_build.rootVisualElement.Q("DialogInfoLabel") as Label;
        buy_button = ui_build.rootVisualElement.Q("BuyButton") as Button;
        buy_button.clicked += () => CheckProgress();
        to_spawn.SetActive(funded);
        fund_label.text = ScriptResourceManager.wood_amount.ToString() + "\n";
        fund_label.text += ScriptResourceManager.stone_amount.ToString() + "\n";
        fund_label.text += ScriptResourceManager.metal_amount.ToString() + "\n";
        fund_label.text += ScriptResourceManager.money.ToString();
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
        if (funded)
        {
            to_spawn.SetActive(funded);
            Destroy(to_spawn);
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
            funded = true;
            to_spawn.SetActive(funded);
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
}
