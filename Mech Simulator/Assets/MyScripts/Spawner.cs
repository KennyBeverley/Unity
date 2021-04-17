using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public float spawnDelay;
    public float minDelay;
    public float maxDelay;
    public GameObject spawn;

    public float nextSpawnTime;


    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + CreateRandomFloat(minDelay, maxDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            Vector3 offset = new Vector3(0, 0, CreateRandomFloat(-10, 10));
            Instantiate(spawn, transform.position + offset, Quaternion.identity);
            nextSpawnTime = Time.time + CreateRandomFloat(minDelay, maxDelay);
        }
    }
    

    public float CreateRandomFloat(float min, float max)
    {
        return Random.Range(min, max);
    }

   
}
