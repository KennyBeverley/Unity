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

    private bool canSpawn;


    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + CreateRandomFloat(minDelay, maxDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            canSpawn = true;
        }

        if (Time.time > nextSpawnTime && canSpawn)
        {
            Vector3 offset = new Vector3(CreateRandomFloat(10, 40), 0, CreateRandomFloat(40, 100));
            Instantiate(spawn, transform.position + offset, Quaternion.identity);
            nextSpawnTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
    

    public float CreateRandomFloat(float min, float max)
    {
        if(Random.Range(0, 2) == 0)
        {
            return Random.Range(-min, -max);
        }
        else
        {
            return Random.Range(min, max);
        }
    }

   
}
