using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechSettings : MonoBehaviour
{
    public GameObject[] mechComponents;
    public GameObject rifle1;
    public GameObject rifle2;
    public Material mat1;
    public Material mat2;

    public Material tranRifle;
    public Material solidRifle;

    public bool isSolid = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        

        if (Input.GetButtonDown("Button4") || Input.GetKeyDown(KeyCode.S))
        {
            if (!isSolid)
            {
                rifle1.GetComponent<Renderer>().material = solidRifle;
                rifle2.GetComponent<Renderer>().material = solidRifle;
                foreach (var val in mechComponents)
                {
                    val.GetComponent<Renderer>().material = mat1;
                    isSolid = true;
                }
            }
            else
            {
                rifle1.GetComponent<Renderer>().material = tranRifle;
                rifle2.GetComponent<Renderer>().material = tranRifle;
                foreach (var val in mechComponents)
                {
                    val.GetComponent<Renderer>().material = mat2;
                    isSolid = false;
                }
            }
            
        }
    }
}
