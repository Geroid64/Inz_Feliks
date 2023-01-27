using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MainPlayerHUD : MonoBehaviour
{

    public UIDocument ui_doc;
    public bool is_player_hud = true;
    //private Label label_stone;
    //private Label label_wood;
    //private Label label_metal;
    //private Label label_money;
    //private Label label_time;
    //private Label label_player_ammo;

    public void UIUpdateLabel(string label_name,string text, int value)
    {
        Label label = ui_doc.rootVisualElement.Q(label_name) as Label;
        
        label.text = text + value.ToString();
    }

    public void UIUpdateTimeLabel(string label_name, float minutes, float seconds)
    {
        Label label = ui_doc.rootVisualElement.Q(label_name) as Label;
        label.text = string.Format("{0:00}",minutes)+":"+string.Format("{0:00}",seconds);
    }
}
