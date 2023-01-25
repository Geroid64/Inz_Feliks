using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    public int scene_index;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("zmiana poziomu na: " + scene_index);
            SceneManager.LoadScene(scene_index, LoadSceneMode.Single);
        }
    }
}
