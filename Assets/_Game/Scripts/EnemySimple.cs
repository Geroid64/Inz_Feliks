using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemySimple : MonoBehaviour
{

    public int damage;
    public string type;

    public RaycastHit raycast;
    public LayerMask mask;
    public Vector3 vector = new Vector3(0, 0, 0);
    public Vector3 dest;

    private NavMeshAgent navMesh;

    private bool coroutine_start = true;
    private bool coroutine_attack = false;

    private Coroutine routine = null;
    private Coroutine routine_attack = null;

    LineRenderer lineee;

    private void Awake()
    {
        navMesh = GetComponentInParent<NavMeshAgent>();
    }

    private void Start()
    {
        routine = StartCoroutine(Scan(dest));
        lineee = GetComponent<LineRenderer>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lineee.enabled = false;
            if (routine_attack != null)
            {
                coroutine_attack = false;
                StopCoroutine(routine_attack);
            }

            if (coroutine_start == false)
            {
                coroutine_start = true;
                routine = StartCoroutine(Scan(dest));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Physics.Raycast(transform.position+vector, (other.transform.position - transform.position).normalized, out raycast, Mathf.Infinity,~mask))
            {
                if (raycast.collider.gameObject.CompareTag("Player")|| raycast.collider.gameObject.CompareTag("PlayerSee"))
                {
                    Debug.Log("HELP==WIDZI");
                    
                    if (routine != null)
                    {
                        StopCoroutine(routine);
                        coroutine_start = false;
                    }
                    
                    if (coroutine_attack==false)
                    {
                        coroutine_attack = true;
                        routine_attack = StartCoroutine(Attack(other.GetComponent<ScriptHealth>()));
                    }

                    navMesh.destination = other.gameObject.transform.position;

                    transform.parent.transform.rotation = Quaternion.LookRotation(other.transform.position - transform.position, Vector3.up);
                }
                else
                {
                    Debug.Log("HELP==NIE_WIDZI");
                    Debug.DrawRay(transform.position+vector, (other.transform.position - transform.position), Color.white);
                    
                    if (routine_attack != null)
                    {
                        coroutine_attack = false;
                        StopCoroutine(routine_attack);
                    }

                    if (coroutine_start == false)
                    {
                        coroutine_start = true;
                        routine = StartCoroutine(Scan(dest));
                    }
                }
            }
        }
    }
    
    IEnumerator Scan(Vector3 dest)
    {
        while(coroutine_start)
        {
            dest = RandomPointNavMesh(transform.position, 10);
            navMesh.destination = dest;
            yield return new WaitForSeconds(Random.Range(2,3));
        }
    }

    IEnumerator Attack(ScriptHealth player)
    {
        Debug.Log("HELPP"+coroutine_attack);
        while(coroutine_attack)
        {
            lineee.SetPosition(0, transform.position);
            lineee.SetPosition(1, player.transform.position);

            lineee.enabled = true;
            yield return new WaitForSeconds(0.1f);
            lineee.enabled = false;


            player.Suffer("damage", 5);
            yield return new WaitForSeconds(1);

        }

    }

    public static Vector3 RandomPointNavMesh(Vector3 start, float range)
    {
        NavMeshHit is_nav;
        Vector3 direction = Random.insideUnitSphere * range;
        direction += start;
        NavMesh.SamplePosition(direction, out is_nav, range, NavMesh.AllAreas);
        return is_nav.position;
    }


}
