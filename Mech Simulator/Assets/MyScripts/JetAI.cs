using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum JetState {descend, hover, seek, fight, die}
public class JetAI : MonoBehaviour
{
    public GameObject explosion;
    public GameObject[] rockets;

    private int rocketsFired = 0;
    private Animator anim;
    private GameObject player;
    [System.NonSerialized]
    public JetState state;
    private float distanceToPlayer;
    private NavMeshAgent nav;

    private float nextRocketFireTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        state = JetState.descend;
        anim = transform.GetChild(0).GetComponent<Animator>();
        nav = gameObject.GetComponentInParent<NavMeshAgent>();
        nav.enabled = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            case JetState.descend:
                transform.parent.transform.position -= new Vector3(0, .1f, 0);
                break;

            case JetState.hover:
                state = JetState.seek;
                    break;

            case JetState.seek:
                distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                if (distanceToPlayer > 15)
                {
                    nav.isStopped = false;
                    //move to target
                    //Debug.Log("move in");
                    nav.SetDestination(player.transform.position);
                    transform.LookAt(player.transform.position);
                }
                if(distanceToPlayer < 15)                    
                {
                    nav.isStopped = true;
                    StartCoroutine(DeployGuns());
                    

                    //play animation
                    state = JetState.fight;
                }

                if (Time.time > nextRocketFireTime && rocketsFired < 4)
                {
                    fireRocket();
                    nextRocketFireTime = Time.time + 3;
                }


                break;

            case JetState.fight:
                distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                //Debug.Log(distanceToPlayer);
                if (Time.time > nextRocketFireTime && rocketsFired < 4)
                {
                    fireRocket();
                    nextRocketFireTime = Time.time + 3;
                }


                if (distanceToPlayer < 13)
                {
                    transform.parent.position += (transform.parent.position + (transform.parent.position - player.transform.position)).normalized * Time.deltaTime * 5;
                    //nav.SetDestination(transform.parent.position + (transform.parent.position - player.transform.position));
                }

                if (distanceToPlayer > 20)
                {
                    nav.isStopped = false;
                    //move to target
                    //Debug.Log("move in");
                    nav.SetDestination(player.transform.position);
                    transform.LookAt(player.transform.position);
                }
                transform.LookAt(player.transform.position);
                break;

            case JetState.die:
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
                break;

        }

        IEnumerator DeployGuns()
        {
            anim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(.5f);
            anim.SetBool("gunsDeployed", true);
        }

        
    }

    private void fireRocket()
    {
        if(rocketsFired < 4)
        {
            rockets[rocketsFired].GetComponent<Rocket>().enabled = true;
            rocketsFired++;
        }
        
    }

}
