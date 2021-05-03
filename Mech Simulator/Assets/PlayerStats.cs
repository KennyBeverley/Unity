using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int MaxHP;
    public GameObject explosion;
    private int currentHp;
    public GameObject groundCheck;
    public bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        currentHp = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        if(currentHp - damage <= 0)
        {
            StartCoroutine(Die());
            currentHp = 0;
            isDead = true;
        }
        else
        {
            currentHp -= damage;
        }
        Debug.Log(currentHp);
    }

   IEnumerator Die()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        GetComponent<CharacterController>().enabled = false;
        transform.GetChild(0).GetComponent<Animator>().Play("Death");
        GetComponent<AudioSource>().Stop();
        groundCheck.SetActive(false);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("SampleScene");

    }
}
