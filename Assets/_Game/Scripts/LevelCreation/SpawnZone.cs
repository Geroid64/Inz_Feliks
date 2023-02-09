using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public string type;

    //public SpawnManager

    void Awake()
    {
        switch (type)
        {
            case "health":
                SpawnManager.available_health_spawn_zones.Add(this.gameObject);
                break;
            case "stone":
                SpawnManager.available_stone_spawn_zones.Add(this.gameObject);
                break;
            case "wood":
                SpawnManager.available_wood_spawn_zones.Add(this.gameObject);
                break;
            case "metal":
                SpawnManager.available_metal_spawn_zones.Add(this.gameObject);
                break;
            case "money":
                SpawnManager.available_money_spawn_zones.Add(this.gameObject);
                break;
            case "enemy":
                SpawnManager.available_enemies_spawn_zones.Add(this.gameObject);
                break;
        }

        
        //Destroy(this);
    }
}
