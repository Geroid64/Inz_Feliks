using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ChooseMissionScript : MonoBehaviour
{
    public UIDocument ui_doc;
    public bool is_choosing_mission = false;
    public string difficulty = "normal";
    public string terrain_type = "Rock";
    public string resource_spread = "stone";
    //names
    //    LowerDifficultyButton
    //    HigherDifficultyButton
    //    PreviousSectorButton
    //    NextSectorButton
    //    ConfirmMissionButton
    //    RandomMissionButton
    //    MissionDebriefingLabel


    private void Awake()
    {
        UIUpdateLabel("MissionDebriefingLabel", "Your mission: Survive ");
    }

    public void UIUpdateLabel(string label_name, string string_text)
    {
        Label label = ui_doc.rootVisualElement.Q(label_name) as Label;

        label.text = string_text;
    }

}
