using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeadTracking : MonoBehaviour
{
    public float rotationSpeed;
    public LayerMask mask;
    public GameObject viewPoint;
    public GameObject leftTurret;
    public GameObject rightTurret;
    public GameObject crosshairs;

    [System.NonSerialized]
    public Vector3 point;
    private GameObject mech;
    private GameObject gm;
    private VRMapping controlls;

    // Start is called before the first frame update
    void Start()
    {
        mech = GameObject.Find("Player");
        gm = GameObject.Find("GameManager");
        controlls = gm.GetComponent<VRMapping>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<PlayerStats>().isDead)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(viewPoint.transform.position);

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
            {
                Transform objectHit = hit.transform;
                if (objectHit.gameObject.layer == 6)
                {
                    //text.text = hit.transform.name + "";
                    crosshairs.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    //text.text = "No Target";
                    crosshairs.GetComponent<Image>().color = Color.red;
                }
                point = hit.point;
                if (objectHit.name == "RightWall")
                {
                    mech.transform.Rotate(0, rotationSpeed, 0);
                }
                if (objectHit.name == "LeftWall")
                {
                    mech.transform.Rotate(0, -rotationSpeed, 0);
                }
                if (objectHit.name == "RightQuarterWall")
                {
                    mech.transform.Rotate(0, rotationSpeed * .5f, 0);
                }
                if (objectHit.name == "LeftQuarterWall")
                {
                    mech.transform.Rotate(0, -rotationSpeed * .5f, 0);
                }



                if (objectHit.gameObject.layer != 8)
                {
                    leftTurret.transform.LookAt(hit.point);
                    rightTurret.transform.LookAt(hit.point);
                }
                else
                {
                    leftTurret.transform.localEulerAngles = new Vector3(0, 0, 0);
                    rightTurret.transform.localEulerAngles = new Vector3(0, 0, 0);
                }

            }
        }
        
    }
}