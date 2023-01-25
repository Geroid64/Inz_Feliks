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
            case "resource":
                SpawnManager.available_resources_spawn_zones.Add(this.gameObject);
                break;
            case "enemy":
                SpawnManager.available_enemies_spawn_zones.Add(this.gameObject);
                break;
        }

        
        //Destroy(this);
    }
}
