using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTile : MonoBehaviour
{
    public GameObject[] objects_to_spawn;
    public GameObject tile;
    public int min, max =0;
    int amount;
    Vector3 random_placement;

    void Start()
    {
        amount = Random.Range(min, max);
        for (int i = 0; i < amount; i++)
        {
            random_placement = new Vector3(
                Random.Range(
                    tile.transform.position.x-(tile.transform.localScale.x)*5, tile.transform.position.x + (tile.transform.localScale.x) *5), 
                0, 
                Random.Range(
                    tile.transform.position.z - (tile.transform.localScale.z * 5), tile.transform.position.z  + (tile.transform.localScale.z * 5)));
            Quaternion roton = Quaternion.Euler(new Vector3(0, Random.Range(-180, 180), 0));
            Instantiate(objects_to_spawn[Random.Range(0, objects_to_spawn.Length)], random_placement,roton);

        }
    }
}
