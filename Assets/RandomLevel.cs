using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomLevel : MonoBehaviour
{
    public bool is_random = true;
    public int biome_index = 0;
    public List<GameObject> Biome = new List<GameObject>();
    List<GameObject> to_spawn = new List<GameObject>();
    public int grid_size_x = 0;
    public int grid_size_y = 0;
    public int step = 10;

    List<int[]> available_entropies = new List<int[]>();

    Dictionary<string, string[]> all_nodes = new Dictionary<string, string[]>();

    GameObject randix;
    string[,][] info_grid;
    bool[,] bool_grid;
    string[] arr;
    int[] active= new int[2];


    void Start()
    {
        //Debug.DrawLine(new Vector3(-5, 0, -5), new Vector3((grid_size_x * step)-5, 0, -5), Color.white, 50);
        //Debug.DrawLine(new Vector3(-5, 0, -5), new Vector3(-5, 0, (grid_size_y*step)-5), Color.white, 50);
        if (is_random)
        {
            randix = Biome[Random.Range(0, Biome.Count())];
        }
        else
        {
            randix = Biome[biome_index];
        }
        
        foreach (Transform tile in randix.transform)
        {
            to_spawn.Add(tile.gameObject);
        }
        
        foreach (GameObject objec in to_spawn)
        {
            all_nodes.Add(objec.GetComponent<LevelSegment>().tile_name, objec.GetComponent<LevelSegment>().can_join);
        }

        info_grid = new string[grid_size_x,grid_size_y][];
        bool_grid = new bool[grid_size_x, grid_size_y];
        for (int i = 0; i< grid_size_x; i ++)
        {
            for (int j = 0; j < grid_size_y; j ++)
            {
                arr = new string[all_nodes.Keys.Count+1];
                arr[0] = "none";
                all_nodes.Keys.CopyTo(arr,1);
                info_grid[i,j] = arr;
                bool_grid[i, j] = false;
            }
        }

        active[0] = Random.Range(0, grid_size_x - 1);
        active[1] = Random.Range(0, grid_size_y - 1);

        do
        {
            SpawnTile(active[0], active[1]);
            ChangeAvailableTile(active[0], active[1]);

            available_entropies = available_entropies.OrderBy(entropies => entropies[0]).ToList();

            active[0] = available_entropies[0][1];//1
            active[1] = available_entropies[0][2];//2

            available_entropies.RemoveAt(0);
        } while (available_entropies.Any());

        SpawnTile(active[0], active[1]);
        Debug.Log(active[0]+" "+active[1]);

        MakeGround();
    }

    public void SpawnTile(int x, int y)
    {
        if (info_grid[x, y].Length >= 2)
        {
            int rand = 0;
            string tile_string;

            if (info_grid[x, y].Length == 2)
            {
                tile_string = info_grid[x, y][1];
            }
            else
            {
                rand = Random.Range(1, info_grid[x, y].Length);
                tile_string = info_grid[x, y][rand];
            }

            info_grid[x, y] = new string[] { tile_string };
            bool_grid[x, y] = true;
            GameObject spawn_place = to_spawn.Find(tile => string.Equals(tile.GetComponent<LevelSegment>().tile_name, tile_string));
            //Debug.Log("WYBRANY TILE: " + spawn_place.GetComponent<LevelSegment>().tile_name +" I jego indeksy "+x+" "+y);
            Instantiate(spawn_place, new Vector3(x * step, 0, y * step), Quaternion.identity);
        }
    }

    public void ChangeAvailableTile(int x, int y)
    {
        if (x - 1 >= 0)
        {
            CheckProx(x - 1, y);
        }
        if (x + 1 < grid_size_x)
        {
            CheckProx(x + 1, y);
        }
        if (y - 1 >= 0)
        {
            CheckProx(x, y - 1);
        }
        if (y + 1 < grid_size_y)
        {
            CheckProx(x, y + 1);
        }
    }

    public void CheckProx(int x, int y)
    {
        if(info_grid[x,y][0]=="none")
        {
            //Debug.Log("ORYGINALNY TILE DO POROWNANIA: " + info_grid[active[0], active[1]][0]);
            string[] c = all_nodes[info_grid[active[0], active[1]][0]];

            if (c != null)
            {
                string[] a = info_grid[x, y].Intersect(c).ToArray();


                string[] temp = new string[a.Length + 1];
                temp[0] = "none";
                a.CopyTo(temp, 1);
                info_grid[x, y] = temp;

                if(bool_grid[x,y]==false)
                {
                    available_entropies.Add(new int[] { info_grid[x, y].Length, x, y });
                    bool_grid[x, y] = true;
                }
                else
                {
                    int[] mhm = new int[] { info_grid[x, y].Length, x, y };
                    int inde =0;
                    for (int i = 0; i < available_entropies.Count; i++)
                    {
                        if(available_entropies[i][1]==x)
                        {
                            if(available_entropies[i][2]==y)
                            {
                                inde = i;
                            }
                        }
                    }
                    available_entropies[inde] = new int[] { info_grid[x, y].Length, x, y };
                }
            }
        }

    }

    public void MakeGround()
    {
        BoxCollider coll = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        coll.transform.position = new Vector3(((grid_size_x * step) / 2)-step/2, 0, ((grid_size_y * step) / 2)-step/2);
        coll.size = new Vector3((grid_size_x*step), 0, (grid_size_y*step));
    }
}