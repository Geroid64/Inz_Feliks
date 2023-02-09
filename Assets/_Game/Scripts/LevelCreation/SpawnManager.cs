using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //======================================

    //======for spawning in
    public static List<GameObject> available_health_spawn_zones = new List<GameObject>();
    public static List<GameObject> available_enemies_spawn_zones = new List<GameObject>();
    public static List<GameObject> available_stone_spawn_zones = new List<GameObject>();
    public static List<GameObject> available_wood_spawn_zones = new List<GameObject>();
    public static List<GameObject> available_metal_spawn_zones = new List<GameObject>();
    public static List<GameObject> available_money_spawn_zones = new List<GameObject>();



    public static List<GameObject> health_spawn = new List<GameObject>();
    public static List<GameObject> enemies_spawn = new List<GameObject>();
    public static List<GameObject> stone_spawn = new List<GameObject>();
    public static List<GameObject> wood_spawn = new List<GameObject>();
    public static List<GameObject> metal_spawn = new List<GameObject>();
    public static List<GameObject> money_spawn = new List<GameObject>();


    public List<GameObject> health = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> stone = new List<GameObject>();
    public List<GameObject> wood = new List<GameObject>();
    public List<GameObject> metal = new List<GameObject>();
    public List<GameObject> money = new List<GameObject>();
    //======================================
    //======for mesh combining
    public static List<GameObject> ground_to_combine = new List<GameObject>();
    public static List<GameObject> tree_to_combine = new List<GameObject>();

    public static List<GameObject> ground_spawn = new List<GameObject>();
    public static List<GameObject> tree_spawn = new List<GameObject>();

    public List<GameObject> ground = new List<GameObject>();
    public List<GameObject> tree = new List<GameObject>();
    //======================================

    public void Awake()
    {
        //======for spawning in
        health_spawn = health;
        enemies_spawn = enemies;
        stone_spawn = stone;
        metal_spawn = metal;
        wood_spawn = wood;
        money_spawn = money;
        //======for mesh combining
        ground_spawn = ground;
        tree_spawn = tree;
    }


}
