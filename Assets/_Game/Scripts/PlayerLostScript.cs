using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerLostScript : MonoBehaviour
{
    //static bool player_lost = false;
    public Camera cam;
    public GameObject fade_plane;
    public ScriptHealth player_health;
    public ScriptResourceManager resource;
    public TMP_Text loosing_text;
    Vector3 offset = new Vector3(0,15,0);

    bool truth = true;
    public void Awake()
    {
        Color color = Color.black;
        color.a = 0;
        fade_plane.GetComponent<MeshRenderer>().material.color = color;
        fade_plane.SetActive(false);
    }

    public void LoserManager()
    {
        //take away 20% of resources
        resource.DeleteResources("wood",(int)(ScriptResourceManager.wood_amount*0.2f));
        resource.DeleteResources("stone",(int)(ScriptResourceManager.stone_amount*0.2f));
        resource.DeleteResources("metal",(int)(ScriptResourceManager.metal_amount*0.2f));

        player_health.hp = player_health.max_hp;
        StartCoroutine(FadeAway());

    }

    IEnumerator FadeAway()
    {
        fade_plane.SetActive(true);
        fade_plane.transform.position = cam.transform.position - offset;
        
        Color color = Color.black;
        color.a = 0;
 
        while (truth)
        {
            color.a += 0.2f;
            fade_plane.GetComponent<MeshRenderer>().material.color = color;
            yield return new WaitForSeconds(0.1f);
            if (color.a>=1)
            {
                truth = false;
            }
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
