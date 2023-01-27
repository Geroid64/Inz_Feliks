using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponBrain : MonoBehaviour
{
    public GameObject ressource_manager;
    public Transform barrel;
    public GameObject bullet;
    public bool is_ammo;
    public bool can_shoot=true;
    public bool is_reloading = false;
    public int mag_size=15;
    public int mag_ammo;
    public float bullet_speed;
    public float time_to_reload = 3;

    public GameObject game_obj_text;
    TMP_Text mesh_text;
    MeshRenderer mesh_render;

    public GameObject obj;
    public GameObject cam;
    public bool reloading;
    public Vector3 text_offset;

    public MainPlayerHUD player_hud;

    public void Start()
    {
        mesh_text =game_obj_text.GetComponent<TMP_Text>();
        mesh_render = game_obj_text.GetComponent<MeshRenderer>();
        mesh_render.enabled = false;
        mag_ammo = 15;
        if (player_hud.is_player_hud)
        {
            player_hud.UIUpdateLabel("MagazineLabel", "", mag_ammo);
        }

        //max_ammo = ressource_manager.GetComponent<ScriptResourceManager>().max_ammo;
    }

    void Update()
    {
        game_obj_text.transform.position = obj.transform.position + text_offset;
        game_obj_text.transform.rotation = cam.transform.rotation;
        if (Input.GetMouseButtonDown(0))
        {

            if (is_ammo)
            {
                if(mag_ammo>0 && can_shoot)
                {
                    Shoot();
                    mag_ammo--;
                }
                else
                {
                    can_shoot = false;
                }
                if (player_hud.is_player_hud)
                {
                    player_hud.UIUpdateLabel("MagazineLabel", "", mag_ammo);
                }
            }

            else
            {
                Shoot();
            }

        }
        if(Input.GetButtonDown("Reload") && !is_reloading)
        {
            if (mag_ammo<mag_size)
            {

                StartCoroutine(Reload());


                Debug.Log("RELOADED");
            }
        }
    }

    IEnumerator Reload()
    {
        while(true)
        {
            ScriptResourceManager ammo_temp = ressource_manager.GetComponent<ScriptResourceManager>();
            can_shoot = false;
            is_reloading = true;
            mesh_render.enabled = true;
            if (mag_ammo<mag_size)
            {
                if (ammo_temp.ammo_amount <= mag_size)
                {
                    if (mag_ammo + ammo_temp.ammo_amount >= mag_size)
                    {

                        mesh_text.text = "Reloading.";
                        yield return new WaitForSeconds(1);
                        mesh_text.text = "Reloading..";
                        yield return new WaitForSeconds(1);
                        mesh_text.text = "Reloading...";
                        yield return new WaitForSeconds(1);
                    
                        ammo_temp.ammo_amount = mag_ammo + ammo_temp.ammo_amount - mag_size;
                        mag_ammo = mag_size;
                    }
                    else if (ammo_temp.ammo_amount != 0)
                    {
                        mesh_text.text = "Reloading.";
                        yield return new WaitForSeconds(1);
                        mesh_text.text = "Reloading..";
                        yield return new WaitForSeconds(1);
                        mesh_text.text = "Reloading...";
                        yield return new WaitForSeconds(1);

                        mag_ammo += ammo_temp.ammo_amount;
                        ammo_temp.ammo_amount = 0;

                    }
                    else if(ammo_temp.ammo_amount==0&& mag_ammo==0)
                    {
                        //Debug.Log("RELOADED NO MORE BULLETS FOR YOU");
                    }
                }
                else
                {
                    ammo_temp.DeleteResources("ammo", mag_size - mag_ammo);

                    mesh_text.text = "Reloading.";
                    yield return new WaitForSeconds(1);
                    mesh_text.text = "Reloading..";
                    yield return new WaitForSeconds(1);
                    mesh_text.text = "Reloading...";
                    yield return new WaitForSeconds(1);
                

                    mag_ammo = mag_size;
                }
            }
            if (player_hud.is_player_hud)
            {
                player_hud.UIUpdateLabel("MagazineLabel", "", mag_ammo);
                player_hud.UIUpdateLabel("AmmoLabel", "", ammo_temp.ammo_amount);
            }


            can_shoot = true;
            is_reloading = false;
            mesh_render.enabled = false;
            break;
        }
    }

    public void Shoot()
    {
        Vector3 direction = transform.forward;
        GameObject spawned_bullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawned_bullet.transform.rotation *= Quaternion.AngleAxis(-90, Vector3.up);
        spawned_bullet.GetComponent<Rigidbody>().AddForce(direction * bullet_speed, ForceMode.Force);
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}