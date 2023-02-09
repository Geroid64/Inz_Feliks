using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarm : MonoBehaviour
{
    public GameObject player;
    ScriptHealth player_health;
    public Collider FOV;
    public float speed = 4f;
    public float rot_speed = 1f;
    public float offset= 1f;
    public int x, y = 50;
    bool wander_bool = true;
    bool attack_bool = false;
    bool damage_bool = false;
    Coroutine routine_wander, routine_attack, routine_deal_damage;
    public Vector3 direction;

    void Start()
    {
        player = GameObject.Find("player");
        player_health = player.GetComponent<ScriptHealth>();
        direction = transform.position;
        States("wander");
    }

    void Update()
    {
        direction.y = transform.position.y;
        if (wander_bool && (direction - transform.position).magnitude >0.001f)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction - transform.position), Time.deltaTime * rot_speed);
        if(attack_bool && (direction - transform.position).magnitude > 0.001f)
            transform.LookAt(player.transform);
        transform.position = Vector3.MoveTowards(transform.position, direction ,speed * Time.deltaTime);

        if (Vector3.Distance(player.transform.position, transform.position) < 5)
        {
            if(damage_bool == false)
            {
                damage_bool = true;
                States("damage");
            }
        }

        else
        {
            damage_bool = false;
            if (routine_deal_damage!=null)
            {
                StopCoroutine(routine_deal_damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            States("attack");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            States("wander");
        }
    }

    public void States(string state)
    {
        switch (state)
        {
            case "wander":
                wander_bool = true;
                attack_bool = false;
                if(routine_attack!=null)
                    StopCoroutine(routine_attack);
                routine_wander = StartCoroutine(Wander());

                break;
            case "attack":
                attack_bool = true;
                wander_bool = false;
                if (routine_wander != null)
                    StopCoroutine(routine_wander);
                routine_attack = StartCoroutine(Attack());
                break;
            case "damage":
                routine_deal_damage = StartCoroutine(DealDamage());
                break;
        }
    }

    IEnumerator Wander()
    {
        while (wander_bool)
        {
            
            direction.x = Random.Range(transform.position.x - offset, transform.position.x + offset);
            direction.z = Random.Range(transform.position.z - offset, transform.position.z + offset);

            yield return new WaitForSeconds(Random.Range(1.5f,3.5f));
        }
    }
    IEnumerator Attack()
    {
        while (attack_bool)
        {
            direction.x = player.transform.position.x;
            direction.z = player.transform.position.z;

            yield return null;
        }
    }
    IEnumerator DealDamage()
    {
        while (damage_bool)
        {
            player_health.Suffer("damage", 5);
            yield return new WaitForSeconds(0.3f);
        }
    }

}
