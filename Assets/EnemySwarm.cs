using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarm : MonoBehaviour
{
    public GameObject player;
    public float speed = 4f;
    public int x, y = 50;
    bool wander_bool = true;
    bool attack_bool = false;
    Coroutine routine_wander, routine_attack;
    public Vector3 direction;

    void Start()
    {
        States("wander");
    }

    void Update()
    {

        transform.Translate(direction * Time.deltaTime * speed);

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
                if(routine_attack!=null)
                    StopCoroutine(routine_attack);
                routine_wander = StartCoroutine(Wander());

                break;
            case "attack":
                attack_bool = true;
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
            float range = 5.5f;

            direction = Random.insideUnitSphere * range;
            direction.y = 0;
            transform.forward = direction.normalized;
            yield return new WaitForSeconds(2);
        }
    }
    IEnumerator Attack()
    {
        while (attack_bool)
        {
            direction = player.transform.position - transform.position;
            transform.forward = direction.normalized;
            yield return null;
        }
    }
}
