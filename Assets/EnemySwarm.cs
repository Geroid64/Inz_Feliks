using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarm : MonoBehaviour
{
    public GameObject player;
    public float speed = 4f;
    public float rot_speed = 1f;
    public float offset= 1f;
    public int x, y = 50;
    bool wander_bool = true;
    bool attack_bool = false;
    Coroutine routine_wander, routine_attack;
    public Vector3 direction;

    void Start()
    {
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
        }
    }

    IEnumerator Wander()
    {
        while (wander_bool)
        {
            direction.x = Random.Range(transform.position.x - offset, transform.position.x + offset);
            direction.z = Random.Range(transform.position.z - offset, transform.position.z + offset);

            yield return new WaitForSeconds(2);
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
}
