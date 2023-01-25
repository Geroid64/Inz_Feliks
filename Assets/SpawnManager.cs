using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static List<GameObject> available_health_spawn_zones = new List<GameObject>();
    public static List<GameObject> available_enemies_spawn_zones = new List<GameObject>();
    public static List<GameObject> available_resources_spawn_zones = new List<GameObject>();

    public static List<GameObject> health_spawn = new List<GameObject>();
    public static List<GameObject> enemies_spawn = new List<GameObject>();
    public static List<GameObject> resources_spawn = new List<GameObject>();

    public List<GameObject> health = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> resources = new List<GameObject>();

    public void Awake()
    {
        health_spawn = health;
        enemies_spawn = enemies;
        resources_spawn = resources;
    }


}
