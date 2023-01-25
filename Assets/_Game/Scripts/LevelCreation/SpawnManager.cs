using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //======================================
    public static int difficulty=1;

    public int difficulty_int = 1;
    //======for spawning in
    public static List<GameObject> available_health_spawn_zones = new List<GameObject>();
    public static List<GameObject> available_enemies_spawn_zones = new List<GameObject>();
    public static List<GameObject> available_resources_spawn_zones = new List<GameObject>();


    public static List<GameObject> health_spawn = new List<GameObject>();
    public static List<GameObject> enemies_spawn = new List<GameObject>();
    public static List<GameObject> resources_spawn = new List<GameObject>();

    public List<GameObject> health = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> resources = new List<GameObject>();
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
        difficulty = difficulty_int;
        //======for spawning in
        health_spawn = health;
        enemies_spawn = enemies;
        resources_spawn = resources;
        //======for mesh combining
        ground_spawn = ground;
        tree_spawn = tree;
    }


}
