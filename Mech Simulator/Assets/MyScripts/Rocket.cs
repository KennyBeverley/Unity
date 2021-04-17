using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject player;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + (player.transform.position - transform.position).normalized * Time.deltaTime * 10;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Shield"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
