using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class checkGround : MonoBehaviour
{
    public GameObject jet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            transform.GetComponentInParent<NavMeshAgent>().enabled = true;
            jet.GetComponent<JetAI>().state = JetState.hover;

        }
    }


}
