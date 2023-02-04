using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public UIDocument pause_menu;
    Button button1, button2, button3;
    bool is_open = false;
    //names
    //    ResumeButton
    //    BackToMenuButton
    //    ExitGameButton

    private void Awake()
    {
        is_open = false;
        pause_menu.rootVisualElement.style.display = DisplayStyle.None;
        button1 = pause_menu.rootVisualElement.Q("ResumeButton") as Button;
        button2 = pause_menu.rootVisualElement.Q("BackToMenuButton") as Button;
        button3 = pause_menu.rootVisualElement.Q("ExitGameButton") as Button;
        button1.clicked += () => ClosePauseMenu();
        button2.clicked += () => ChangeScene(); 
        button3.clicked += () => Application.Quit();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            is_open = !is_open;
            if (is_open)
            {
                Time.timeScale = 0;
                pause_menu.rootVisualElement.style.display = DisplayStyle.Flex;
            }
            else
            {
                Time.timeScale = 1;
                pause_menu.rootVisualElement.style.display = DisplayStyle.None; 
            }
        }
    }

    public void ClosePauseMenu()
    {

        is_open = false;
        pause_menu.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void ChangeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
