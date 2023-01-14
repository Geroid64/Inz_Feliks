using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBrain : MonoBehaviour
{
    public GameObject ressource_manager;
    public Transform barrel;
    public GameObject bullet;
    public bool is_ammo;
    public bool can_shoot=true;
    public bool is_reloading = false;
    public int mag_size=15;
    private int mag_ammo;
    public float bullet_speed;
    public float time_to_reload = 3;

    public void Start()
    {
        mag_ammo = 15;
        //max_ammo = ressource_manager.GetComponent<ScriptResourceManager>().max_ammo;
    }

    void Update()
    {
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
                    Debug.Log("brak ammunicji "+ mag_ammo);
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

            if(mag_ammo<mag_size)
            {
                if (ammo_temp.ammo_amount <= mag_size)
                {
                    if (mag_ammo + ammo_temp.ammo_amount >= mag_size)
                    {
                        yield return new WaitForSeconds(time_to_reload);
                        ammo_temp.ammo_amount = mag_ammo + ammo_temp.ammo_amount - mag_size;
                        mag_ammo = mag_size;
                    }
                    else if (ammo_temp.ammo_amount != 0)
                    {
                        yield return new WaitForSeconds(time_to_reload);
                        mag_ammo += ammo_temp.ammo_amount;
                        ammo_temp.ammo_amount = 0;
                    }
                    else if(ammo_temp.ammo_amount==0&& mag_ammo==0)
                    {
                        Debug.Log("RELOADED NO MORE BULLETS FOR YOU");
                    }

                }
                else
                {
                    ammo_temp.DeleteResources("ammo", mag_size - mag_ammo);
                    yield return new WaitForSeconds(time_to_reload);
                    mag_ammo = mag_size;
                }
            }

            Debug.Log("RELOADED THIS MUCH: " + (mag_size - mag_ammo) + "LEFT AMMO: " + ammo_temp.ammo_amount);

            Debug.Log("RELOADED IN MAG===:" + mag_ammo);
            can_shoot = true;
            is_reloading = false;
            break;
        }
    }

    public void Shoot()
    {
        Vector3 direction = transform.forward;
        GameObject spawned_bullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawned_bullet.GetComponent<Rigidbody>().AddForce(direction * bullet_speed, ForceMode.Force);
    }
}