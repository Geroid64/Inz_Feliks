using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildInterior : MonoBehaviour
{
    public Camera cam;
    public GameObject to_spawn;
    public bool funded = false;
    public UIDocument ui_build;
    GameObject project;


    private void Awake()
    {
        ui_build.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void OnTriggrerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ui_build.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

    public void CheckProgress()
    {

    }
}
