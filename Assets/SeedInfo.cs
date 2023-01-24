using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedInfo : MonoBehaviour
{
    public int seed = 0;
    public string seed_string;
    public bool is_random;

    void Awake()
    {
        if (!is_random)
            seed = seed_string.GetHashCode();
        else
        {
            seed_string =  System.DateTime.Now.Ticks.ToString();
            seed = (int)System.DateTime.Now.Ticks;
        }
        Random.InitState(seed);
    }
}
