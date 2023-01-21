using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomLevel : MonoBehaviour
{
    public List<GameObject> to_spawn = new List<GameObject>();
    public int grid_size_x = 0;
    public int grid_size_y = 0;
    public int step = 10;
    List<int[]> available_entropies = new List<int[]>();
    int dbg=0;
    int number_of_tiles;
    string[,][] info_grid;
    string[] arr;
    int[] active= new int[2];
    Dictionary<string, string[]> all_nodes = new Dictionary<string, string[]>();

    void Start()
    {
        Debug.DrawLine(new Vector3(-5, 0, -5), new Vector3((grid_size_x * step)-5, 0, -5), Color.white, 50);
        Debug.DrawLine(new Vector3(-5, 0, -5), new Vector3(-5, 0, (grid_size_y*step)-5), Color.white, 50);
        foreach (GameObject objec in to_spawn)
        {
            Debug.Log(objec);
            all_nodes.Add(objec.GetComponent<LevelSegment>().tile_name, objec.GetComponent<LevelSegment>().can_join);
        }

        number_of_tiles = all_nodes.Count;

        info_grid = new string[grid_size_x,grid_size_y][];
        for (int i = 0; i< grid_size_x; i ++)
        {
            for (int j = 0; j < grid_size_y; j ++)
            {
                arr = new string[all_nodes.Count+1];
                arr[0] = "none";
                all_nodes.Keys.CopyTo(arr,1);
                info_grid[i,j] = arr;
            }
        }

        active[0] = Random.Range(0, grid_size_x - 1);
        active[1] = Random.Range(0, grid_size_y - 1);
        for (int i = 0; i < 9; i++)
        {
            SpawnTile(active[0], active[1]);
            ChangeAvailableTile(active[0], active[1]);
            available_entropies = available_entropies.OrderBy(entropies => entropies[0]).ToList();
            active[0] = available_entropies[0][1];
            active[1] = available_entropies[0][2];
            available_entropies.RemoveAt(0);
        }

        Destroy(gameObject);

    }

    public void SpawnTile(int x, int y)
    {
        Debug.Log("---------- " + dbg + " ---------------");
        Debug.Log("x i y: " + x + " " + y);
        Debug.Log(" == " + Random.Range(1, info_grid[x, y].Length));
        for (int i = 0; i < info_grid[x,y].Length; i++)
        {
            Debug.Log("[[[[[[[[[["+info_grid[x, y][i]);
        }
        Debug.Log(info_grid[x, y].Length);
        //Debug.Log(info_grid[x, y][Random.Range(1, info_grid[x, y].Length)]);
        Debug.Log("--------------------------------------");
        dbg++;
        int rand = Random.Range(1, info_grid[x, y].Length);
        string tile_string = info_grid[x, y][rand];
        info_grid[x, y] =new string[]{tile_string};
        GameObject spawn_place = to_spawn.Find(tile => string.Equals(tile.GetComponent<LevelSegment>().tile_name,tile_string));
        Instantiate(spawn_place, new Vector3(x * step, 0, y * step), Quaternion.identity);
    }

    public void ChangeAvailableTile(int x, int y)
    {
        if (y - 1 >= 0)
        {
            CheckProx(x, y - 1);
        }

        if (y + 1 < grid_size_y)
        {
            CheckProx(x, y + 1);
        }

        if (x - 1 >= 0)
        {
            CheckProx(x - 1, y);
        }

        if (x + 1 < grid_size_x)
        {
            CheckProx(x + 1, y);
        }

    }

    public void CheckProx(int x, int y)
    {

        if (info_grid[x, y].Length > 1)
        {
            string[] c = all_nodes[info_grid[active[0], active[1]][0]];

            if (c != null && c[0] != "none")
            {
                string[] a = info_grid[x, y].Intersect(c).ToArray();
                string[] temp = new string[a.Length + 1];
                temp[0] = "none";
                a.CopyTo(temp, 1);
                info_grid[x, y] = temp;
                available_entropies.Add(new int[]{info_grid[x,y].Length,x,y});
            }
        }
    }

}