using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public float time = 0.3f;
    public List<GameObject> resource = new List<GameObject>();
    public int node_life;
    bool stop = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stop = false;
            StartCoroutine(SpawnResource());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stop = true;
        }
    }

    IEnumerator SpawnResource()
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
        GameObject res = Instantiate(resource[Random.Range(0, resource.Count)]);
        res.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        Rigidbody resrb = res.GetComponent<Rigidbody>();
        
        resrb.AddForce(Random.Range(-5f,5f), Random.Range(1f,2f), Random.Range(-5f,5f), ForceMode.Impulse);
    }
}
