using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int hp;
    public int damage;

    private JetAI ai;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<JetAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if(hp - damage <= 0)
        {
            ai.state = JetState.die;
            //die
        }
        else
        {
            hp -= damage;
        }
    }

    
}
