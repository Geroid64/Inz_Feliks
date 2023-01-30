using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public UIDocument ui_doc;
    public UIDocument ui_credits;
    Button credits_button, settings_button, continue_button, start_button;
    Button return_button;
    //names
    //    CreditsButton
    //    SettingsButton
    //    ContinueButton
    //    StartButton

    void OnEnable()
    {

        
        ui_credits.rootVisualElement.style.display = DisplayStyle.None;
        credits_button = ui_doc.rootVisualElement.Q("CreditsButton") as Button;
        settings_button = ui_doc.rootVisualElement.Q("SettingsButton") as Button;
        continue_button = ui_doc.rootVisualElement.Q("ContinueButton") as Button;
        start_button = ui_doc.rootVisualElement.Q("StartButton") as Button;
        credits_button.clicked += () => ShowCredits();
        settings_button.clicked += () => ShowSettings();
        continue_button.clicked += () => ContinueGame();
        start_button.clicked += () => StartNewGame();
    }

    public void ShowCredits()
    {
        ui_doc.rootVisualElement.style.display = DisplayStyle.None;
        ui_credits.rootVisualElement.style.display = DisplayStyle.Flex;
        return_button = ui_doc.rootVisualElement.Q("ReturnButton") as Button;
        return_button.clicked += () => HideCredits();





 

        //button 1
        //scrolling with textmesh
        //button 2
    }
    public void HideCredits()
    {
        Debug.Log("MMM");
        ui_credits.rootVisualElement.style.display = DisplayStyle.None;
        ui_doc.rootVisualElement.style.display = DisplayStyle.Flex;

    }
    public void ShowSettings()
    {
        //volume
    }
    
    public void ContinueGame()
    {
        //saves == txt file with resource amounts, money, and grid placements
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
