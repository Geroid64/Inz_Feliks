using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicZone : MonoBehaviour
{
    public Camera cam;
    public float default_camera_size = 7.8f;
    public float target_size = 20f;
    public float time_change_s = 3f;
    public float time_return_s = 1f;
    private Coroutine routine = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("--------------");
            if (routine != null)
                StopCoroutine(routine);

            routine = StartCoroutine(ChangeCameraSize(time_change_s,cam.orthographicSize, target_size));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("++++++++++++++");
            if(routine != null)
                StopCoroutine(routine);

            routine = StartCoroutine(ChangeCameraSize(time_return_s,cam.orthographicSize, default_camera_size));
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
