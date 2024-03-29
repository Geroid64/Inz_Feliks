using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class RandomLevel : MonoBehaviour
{

 //   public static int biome_index = 0;

    public SeedInfo seed_info;


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
        if (ChooseMissionScript.is_random_biome)
        {
            randix = ChooseMissionScript.biome[Random.Range(0, ChooseMissionScript.biome.Count())];
        }
        else
        {
            randix = ChooseMissionScript.biome[ChooseMissionScript.biome_index];
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

        MakeGround();

        SpawnStuff();
        CleanUp();
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
            GameObject new_tile = Instantiate(spawn_place, new Vector3(x * step, 0, y * step), Quaternion.identity);

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
        BoxCollider coll = gameObject.GetComponent<BoxCollider>();//gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        coll.transform.position = new Vector3(((grid_size_x * step) / 2)-step/2, -0.5f, ((grid_size_y * step) / 2)-step/2);
        coll.size = new Vector3((grid_size_x*step), 1, (grid_size_y*step));
    }

    public void SpawnStuff()
    {
        //health
        for (int i = 0; i < DifficultyPresetsManager.max_health; i++)
        {
            int count = SpawnManager.available_health_spawn_zones.Count;
            GameObject to_spawn = SpawnManager.available_health_spawn_zones[Random.Range(0, count)];
            Quaternion roton = Quaternion.Euler(new Vector3(0, Random.Range(-180, 180), 0));
            Instantiate(SpawnManager.health_spawn[Random.Range(0, SpawnManager.health_spawn.Count-1)], new Vector3(to_spawn.transform.position.x, 0, to_spawn.transform.position.z), roton);
        }
        //resources

        if (SpawnManager.available_wood_spawn_zones.Any())
        {
            for (int i = 0; i < DifficultyPresetsManager.max_wood; i++)
            {
                GameObject to_spawn = SpawnManager.available_wood_spawn_zones[Random.Range(0, SpawnManager.available_wood_spawn_zones.Count)];
                Quaternion roton = Quaternion.Euler(new Vector3(90, Random.Range(-180, 180), 0));
                Instantiate(SpawnManager.wood_spawn[Random.Range(0, SpawnManager.wood_spawn.Count)], new Vector3(to_spawn.transform.position.x, 0, to_spawn.transform.position.z), roton);
            }
        }

        if (SpawnManager.available_stone_spawn_zones.Any())
        {
            for (int i = 0; i < DifficultyPresetsManager.max_stone; i++)
            {
                GameObject to_spawn = SpawnManager.available_stone_spawn_zones[Random.Range(0, SpawnManager.available_stone_spawn_zones.Count)];
                Quaternion roton = Quaternion.Euler(new Vector3(90, Random.Range(-180, 180), 0));
                Instantiate(SpawnManager.stone_spawn[Random.Range(0, SpawnManager.stone_spawn.Count)], new Vector3(to_spawn.transform.position.x, 0, to_spawn.transform.position.z), roton);
            }
        }


        if (SpawnManager.available_metal_spawn_zones.Any())
        {
            for (int i = 0; i < DifficultyPresetsManager.max_metal; i++)
            {
                GameObject to_spawn = SpawnManager.available_metal_spawn_zones[Random.Range(0, SpawnManager.available_metal_spawn_zones.Count)];
                Quaternion roton = Quaternion.Euler(new Vector3(90, Random.Range(-180, 180), 0));
                Instantiate(SpawnManager.metal_spawn[Random.Range(0, SpawnManager.metal_spawn.Count)], new Vector3(to_spawn.transform.position.x, 0, to_spawn.transform.position.z), roton);
            }
        }
        if (SpawnManager.available_money_spawn_zones.Any())
        {
            for (int i = 0; i < DifficultyPresetsManager.max_money; i++)
            {
                GameObject to_spawn = SpawnManager.available_money_spawn_zones[Random.Range(0, SpawnManager.available_money_spawn_zones.Count)];
                Quaternion roton = Quaternion.Euler(new Vector3(90, Random.Range(-180, 180), 0));
                Instantiate(SpawnManager.money_spawn[Random.Range(0, SpawnManager.money_spawn.Count)], new Vector3(to_spawn.transform.position.x, 0, to_spawn.transform.position.z), roton);
            }
        }
        //enemies
        if (SpawnManager.available_enemies_spawn_zones.Any())
        {
            for (int i = 0; i < DifficultyPresetsManager.max_enemy; i++)
            {
                GameObject to_spawn = SpawnManager.available_enemies_spawn_zones[Random.Range(0, SpawnManager.available_enemies_spawn_zones.Count)];
                Quaternion roton = Quaternion.Euler(new Vector3(90, Random.Range(-180, 180), 0));
                Instantiate(SpawnManager.enemies_spawn[Random.Range(0, SpawnManager.enemies_spawn.Count)], new Vector3(to_spawn.transform.position.x, 1, to_spawn.transform.position.z), roton);
            }
        }
    }

    public void CleanUp()
    {
        foreach (GameObject spawn_location in SpawnManager.available_health_spawn_zones)
        {
            Destroy(spawn_location);
        }
        foreach (GameObject spawn_location in SpawnManager.available_enemies_spawn_zones)
        {
            Destroy(spawn_location);
        }
        foreach (GameObject spawn_location in SpawnManager.available_stone_spawn_zones)
        {
            Destroy(spawn_location);
        }
        foreach (GameObject spawn_location in SpawnManager.available_wood_spawn_zones)
        {
            Destroy(spawn_location);
        }
        foreach (GameObject spawn_location in SpawnManager.available_metal_spawn_zones)
        {
            Destroy(spawn_location);
        }
        foreach (GameObject spawn_location in SpawnManager.available_money_spawn_zones)
        {
            Destroy(spawn_location);
        }
        SpawnManager.available_health_spawn_zones.Clear();
        SpawnManager.available_enemies_spawn_zones.Clear();
        SpawnManager.available_stone_spawn_zones.Clear();
        SpawnManager.available_wood_spawn_zones.Clear();
        SpawnManager.available_metal_spawn_zones.Clear();
        SpawnManager.available_money_spawn_zones.Clear();
    }
}