using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicZone : MonoBehaviour
{
    public Camera cam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("--------------");
            StartCoroutine(ChangeCameraSize(3f,cam.orthographicSize, 20f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("++++++++++++++");
            StartCoroutine(ChangeCameraSize(3f,cam.orthographicSize,7.8f));
        }
    }

    IEnumerator ChangeCameraSize(float time, float original, float size)
    {
        float elapsed = 0.0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
            cam.orthographicSize = Mathf.Lerp(original, size, t);

            Debug.Log("KKKKKKKK " + elapsed+ " : " + Time.deltaTime / time + " : " + cam.orthographicSize);


            yield return null;
        }

    }
}
