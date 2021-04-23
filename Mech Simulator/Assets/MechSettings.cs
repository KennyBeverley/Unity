using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechSettings : MonoBehaviour
{
    public GameObject[] mechComponents;
    public Material mat1;
    public Material mat2;

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
                foreach (var val in mechComponents)
                {
                    val.GetComponent<Renderer>().material = mat1;
                    isSolid = true;
                }
            }
            else
            {
                foreach (var val in mechComponents)
                {
                    val.GetComponent<Renderer>().material = mat2;
                    isSolid = false;
                }
            }
            
        }
    }
}
