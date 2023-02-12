using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyPresetsManager : MonoBehaviour
{
    public static bool is_random_difficulty=true;
    public static string[] difficulty;
    public string[] difficulty_string;
    public static int difficulty_index = 0;

    public static string[] resource_spread;
    public string[] resource_spread_string;
    public static int resource_spread_index = 0;

    public static int max_health;
    public static int max_stone;
    public static int max_wood;
    public static int max_metal;
    public static int max_money;
    public static int max_enemy;

    public void Awake()
    {
        if (is_random_difficulty)
        {
            difficulty_index = Random.Range(0, difficulty_string.Length);
            resource_spread_index = Random.Range(0, resource_spread_string.Length);
        }


        difficulty = difficulty_string;
        resource_spread = resource_spread_string;

        switch (difficulty[difficulty_index])
        {
            case "easy":
                max_enemy = 30;
                max_health = 15;
                max_money = 15;
                break;
            case "normal":
                max_enemy = 50;
                max_health = 20;
                max_money = 20;
                break;
            case "hard":
                max_enemy = 80;
                max_health = 10;
                max_money = 35;
                break;
        }

        switch (resource_spread[resource_spread_index])
        {
            case "stone":
                max_stone = 35;
                max_wood = 10;
                max_metal = 10;
                break;
            case "wood":
                max_stone = 10;
                max_wood = 25;
                max_metal = 10;
                break;
            case "metal":
                max_stone = 10;
                max_wood = 13;
                max_metal = 30;
                break;
            case "equal":
                max_stone = 20;
                max_wood = 20;
                max_metal = 20;
                break;
        }
    }
}
