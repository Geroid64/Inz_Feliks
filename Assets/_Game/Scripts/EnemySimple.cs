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
    public Transform dest;

    private NavMeshAgent navMesh;

    private void Awake()
    {
        navMesh = GetComponentInParent<NavMeshAgent>();
    }

    private void Update()
    {
        navMesh.destination = dest.position;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("WWWW");
            if (Physics.Raycast(transform.position+vector, (other.transform.position - transform.position).normalized, out raycast, Mathf.Infinity,~mask))
            {
                if (raycast.collider.gameObject.CompareTag("Player")|| raycast.collider.gameObject.CompareTag("PlayerSee"))
                {
                    Debug.Log("WWWWWWWIDAC" + raycast.collider.gameObject.name);
                    Debug.DrawRay(transform.position+vector, (other.transform.position - transform.position), Color.black);
                }

                else
                {
                    Debug.Log("WWWWWWW===NIEEEEE--IDAC" + raycast.collider.gameObject.name);
                    Debug.DrawRay(transform.position+vector, (other.transform.position - transform.position), Color.white);
                }
            }


                
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
