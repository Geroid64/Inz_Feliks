using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceNode : MonoBehaviour
{
    public float time = 0.3f;
    public List<GameObject> ressource = new List<GameObject>();
    public int node_life;
    bool stop = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stop = false;
            StartCoroutine(SpawnRessource());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stop = true;
        }
    }

    IEnumerator SpawnRessource()
    {

        for (int i = node_life; i > 0; i--)
        {
            if (stop)
                break;
            SpawnOne();
            node_life--;
            yield return new WaitForSeconds(time);

        }

        if (node_life<=0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SpawnOne()
    {
        GameObject ress = Instantiate(ressource[Random.Range(0, ressource.Count)]);
        ress.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        Rigidbody ressrb = ress.GetComponent<Rigidbody>();
        
        ressrb.AddForce(Random.Range(-5f,5f), Random.Range(1f,2f), Random.Range(-5f,5f), ForceMode.Impulse);


    }
}
