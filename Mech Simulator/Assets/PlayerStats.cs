using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int MaxHP;
    private int currentHp;


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
        }
        else
        {
            currentHp -= damage;
        }
        Debug.Log(currentHp);
    }

   IEnumerator Die()
    {

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("SampleScene");

    }
}
