using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
public class ChooseMissionScript : MonoBehaviour
{
    public List<GameObject> biome_non_static = new List<GameObject>();
    public static int biome_index = 1;
    public static List<GameObject> biome = new List<GameObject>();
    public UIDocument ui_doc;
    public DifficultyPresetsManager difficulty_settings;
    public TMP_Text pop_up_text;
    public bool is_choosing_mission = false;
    public bool is_random_biome_ref = false;
    public static bool is_random_biome = false;
    bool in_collider = false;
    public string difficulty = "normal";
    public string terrain_type = "Rock";
    public string resource_spread = "stone";
    public int index_d, index_b=0;
    Button button1, button2, button3, button4, button5, button6;

    //names
    //    LowerDifficultyButton
    //    HigherDifficultyButton
    //    PreviousSectorButton
    //    NextSectorButton
    //    ConfirmMissionButton
    //    RandomMissionButton
    //    MissionDebriefingLabel

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (in_collider)
            {
                is_choosing_mission = !is_choosing_mission;
                if (is_choosing_mission)
                {
                    ui_doc.rootVisualElement.style.display = DisplayStyle.Flex;
                }
                else
                    ui_doc.rootVisualElement.style.display = DisplayStyle.None;
                Debug.Log("FFFFFF" + is_choosing_mission);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pop_up_text.enabled = true;
            in_collider = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        in_collider = false;
        is_choosing_mission = false;
        ui_doc.rootVisualElement.style.display = DisplayStyle.None;
        pop_up_text.enabled = false;
    }

    private void Awake()
    {
        ui_doc.rootVisualElement.style.display = DisplayStyle.None;
        pop_up_text.text = "[F]";
        pop_up_text.enabled = false;
    }
    private void OnEnable()
    {
        is_random_biome = is_random_biome_ref;
        biome = biome_non_static;
        button1 = ui_doc.rootVisualElement.Q("HigherDifficultyButton") as Button;
        button2 = ui_doc.rootVisualElement.Q("LowerDifficultyButton") as Button;
        button3 = ui_doc.rootVisualElement.Q("PreviousSectorButton") as Button;
        button4 = ui_doc.rootVisualElement.Q("NextSectorButton") as Button;
        button5 = ui_doc.rootVisualElement.Q("RandomMissionButton") as Button;
        button6 = ui_doc.rootVisualElement.Q("ConfirmMissionButton") as Button;
        button1.clicked += () => ChangeDifficultyPresets(true);
        button2.clicked += () => ChangeDifficultyPresets(false);
        button3.clicked += () => ChangeSectorPresets(true);
        button4.clicked += () => ChangeSectorPresets(false);
        button5.clicked += () => RandomPresets();
        button6.clicked += () => ConfirmMission();
        UIUpdateLabel("MissionDebriefingLabel", "Your goal: take as many resources as you can in the given time. Good luck!");
    }

    #region Difficulty
    public void ChangeDifficultyPresets(bool change)
    {
        if (is_choosing_mission)
        {
            if (change)
            {
                index_d++;
                if (index_d >= DifficultyPresetsManager.difficulty.Length)
                {
                    index_d = 0;
                }
            }

            else
            {
                index_d--;
                if (index_d < 0)
                {
                    index_d = DifficultyPresetsManager.difficulty.Length-1;
                }
            }
            DifficultyPresetsManager.difficulty_index = index_d;
            UIUpdateLabel("MissionDebriefingLabel", DifficultyPresetsManager.difficulty[index_d]);
            Debug.Log("FFFFF" + DifficultyPresetsManager.difficulty[DifficultyPresetsManager.difficulty_index]);
        }

    }
    #endregion
    #region Sector
    public void ChangeSectorPresets(bool change)
    {
        Debug.Log("OOOOOOOOOO"+biome.Count);
        if (is_choosing_mission)
        {
            if (change)
            {
                index_b++;
                if (index_b >= biome.Count)
                {
                    index_b = 0;
                }
            }

            else
            {
                index_b--;
                if (index_b < 0)
                {
                    index_b = biome.Count - 1;
                }
            }
            biome_index = index_b;
            UIUpdateLabel("MissionDebriefingLabel", biome[biome_index].name);
            Debug.Log("FFFFF" + biome[biome_index]);
        }
    }
    #endregion
    public void RandomPresets()
    {

    }
    public void ConfirmMission()
    {
        ui_doc.rootVisualElement.style.display = DisplayStyle.None;
        is_choosing_mission = false;
    }
    public void UIUpdateLabel(string label_name, string string_text)
    {
        Label label = ui_doc.rootVisualElement.Q(label_name) as Label;
        label.text = string_text;
    }
}
