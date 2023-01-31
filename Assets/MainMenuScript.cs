using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenuScript : MonoBehaviour
{
    public UIDocument ui_doc;
    Button credits_button, settings_button, continue_button, start_button;
    Button return_button;
    VisualElement return_background;
    public TMP_Text credits_text;
    public GameObject credits_game_object;
    Coroutine routine;
    Vector3 credits_initial;
    //names
    //    CreditsButton
    //    SettingsButton
    //    ContinueButton
    //    StartButton
    //    ReturnButton

    void OnEnable()
    {
        credits_initial = credits_text.transform.position;
        credits_button = ui_doc.rootVisualElement.Q("CreditsButton") as Button;
        settings_button = ui_doc.rootVisualElement.Q("SettingsButton") as Button;
        continue_button = ui_doc.rootVisualElement.Q("ContinueButton") as Button;
        start_button = ui_doc.rootVisualElement.Q("StartButton") as Button;
        return_button = ui_doc.rootVisualElement.Q("ReturnButton") as Button;

        return_background = ui_doc.rootVisualElement.Q("ReturnButtonBackground") as VisualElement;

        return_button.clicked += () => HideCredits();
        credits_button.clicked += () => ShowCredits();
        settings_button.clicked += () => ShowSettings();
        continue_button.clicked += () => ContinueGame();
        start_button.clicked += () => StartNewGame();
        credits_game_object.SetActive(false);
        return_background.SetEnabled(false);
        return_button.SetEnabled(false);
        return_background.visible = false;

    }

    public void ShowCredits()
    {
        return_background.visible = true;
        return_background.SetEnabled(true);
        return_button.SetEnabled(true);
        credits_game_object.SetActive(true);
        routine = StartCoroutine(MoveCredits(true));
    }
    public void HideCredits()
    {
        return_background.SetEnabled(false);
        return_button.SetEnabled(false);
        return_background.visible = false;
        credits_game_object.SetActive(false);
        StopCoroutine(routine);
        credits_text.transform.position = credits_initial;
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

    IEnumerator MoveCredits(bool is_scrolling)
    {
        while(true)
        {
            credits_text.transform.position += Vector3.up * Time.deltaTime;
            yield return null;
        }
    }
}
