using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIDefault : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private UIDocument ui_doc;
    
    private Label label;
    private Button take_dmg, get_ressource;
    
    private void OnEnable()
    {
        take_dmg = ui_doc.rootVisualElement.Q("Btake_damage") as Button;
        take_dmg.clicked += () => player.GetComponent<ScriptHealth>().GainStat("health",20);
        get_ressource = ui_doc.rootVisualElement.Q("Bressource") as Button;
        get_ressource.clicked += () => player.GetComponent<ScriptRessourceManager>().DeleteRessources("stone", 5);
    }

    public void UIUpdateLabel(string label_name,int value)
    {
        label = ui_doc.rootVisualElement.Q(label_name) as Label;
        label.text = value.ToString();
    }
}
