using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UIDefault : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private UIDocument ui_doc;
    public bool Dev = true;
    //DEV side--------------------------------------------
    #region DEV
    private Label label;
    private Button take_dmg, get_ressource;
    #endregion
    //USER side-------------------------------------------
    #region USER
    #endregion
    //CODE------------------------------------------------
    private void OnEnable()
    {
        if(Dev)
        {
            take_dmg = ui_doc.rootVisualElement.Q("Btake_damage") as Button;
            take_dmg.clicked += () => player.GetComponent<ScriptHealth>().GainStat("health", 20);
            get_ressource = ui_doc.rootVisualElement.Q("Bressource") as Button;
            get_ressource.clicked += () => player.GetComponent<ScriptResourceManager>().GainResources("stone", 5);
            UIUpdateLabel("Lmoney", 20);
            UIUpdateLabel("Lwood", 20);
            //UIUpdateLabel("Lstone", 10);
            UIUpdateLabel("Lmetal", 60);
            UIUpdateLabel("Lwool", 3);


        }
    }

    public void UIUpdateLabel(string label_name,int value)
    {
        label = ui_doc.rootVisualElement.Q(label_name) as Label;
        label.text = value.ToString();
    }
}
