using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyPresetsManager : MonoBehaviour
{
    public bool is_random=true;
    public static string[] difficulty;
    public string[] difficulty_string;
    public int difficulty_index = 0;

    public static string[] resource_spread;
    public string[] resource_spread_string;
    public int resource_spread_index = 0;

    public static int max_health;
    public static int max_stone;
    public static int max_wood;
    public static int max_enemy;

    public void Awake()
    {
        if (is_random)
        {
            difficulty_index = Random.Range(0, difficulty_string.Length);
            resource_spread_index = Random.Range(0, resource_spread_string.Length);
        }


        difficulty = difficulty_string;
        resource_spread = resource_spread_string;

        switch (difficulty[difficulty_index])
        {
            case "easy":
                max_enemy = 10;
                max_health = 15;
                break;
            case "normal":
                max_enemy = 10;
                max_health = 10;
                break;
            case "hard":
                max_enemy = 20;
                max_health = 5;
                break;
        }

        switch (resource_spread[resource_spread_index])
        {
            case "stone":
                max_stone = 20;
                max_wood = 4;
                break;
            case "wood":
                max_stone = 6;
                max_wood = 25;
                break;
            case "equal":
                max_stone = 10;
                max_wood = 10;
                break;
        }
        Debug.Log("ehhh" + max_stone);
    }
}
