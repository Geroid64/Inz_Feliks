using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    bool to_right = true;

    void Update()
    {
        if (transform.position.x <= -18)
        {
            to_right = false;
        }
        if (transform.position.x >= -15)
        {
            to_right = true;
        }

        if (to_right)
            transform.Translate(new Vector3(-12f,0,0) * 0.02f * Time.deltaTime);
        else
            transform.Translate(new Vector3(12f, 0, 0f) * 0.02f * Time.deltaTime);
    }
}
