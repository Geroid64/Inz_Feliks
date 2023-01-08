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
    public bool PlayerNotFound = true;
    private NavMeshAgent navMesh;
    private Coroutine routine=null;
    private void Awake()
    {
        navMesh = GetComponentInParent<NavMeshAgent>();
    }

    private void Start()
    {
        routine = StartCoroutine(Scan(dest));

    }
    private void Update()
    {


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerNotFound = false;
            Debug.Log("WWWW");
            if (Physics.Raycast(transform.position+vector, (other.transform.position - transform.position).normalized, out raycast, Mathf.Infinity,~mask))
            {
                if (raycast.collider.gameObject.CompareTag("Player")|| raycast.collider.gameObject.CompareTag("PlayerSee"))
                {
                    
                    //StopCoroutine(routine);
                    navMesh.destination = other.gameObject.transform.position;
                    Debug.Log("WWWWWWWIDAC" + raycast.collider.gameObject.name);
                    Debug.DrawRay(transform.position+vector, (other.transform.position - transform.position), Color.black);
                }

                else
                {
                    Debug.Log("WWWWWWW===NIEEEEE--IDAC" + raycast.collider.gameObject.name);
                    Debug.DrawRay(transform.position+vector, (other.transform.position - transform.position), Color.white);
                    PlayerNotFound = true;
                    //routine = StartCoroutine(Scan(dest));
                }
            }


                
        }
    }

    IEnumerator Scan(Vector3 dest)
    {
        while(true)
        {
            dest = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
            Debug.Log("EEEEEEEHHHHH");
            navMesh.destination = dest;
            yield return new WaitForSeconds(3);
        }

    }



    public void EnemyMood(string state)
    {
        switch (state)
        {
            case "passive":
                break;
            case "explore":
                break;
            case "agitated":
                break;
            case "agressive":
                break;
            case "scared":
                break;
        }
    }

}
