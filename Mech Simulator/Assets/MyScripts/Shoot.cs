using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Shoot : MonoBehaviour
{
    public TextMeshProUGUI pAmmoDisplay;

    public GameObject leftFirePoint;
    public GameObject rightFirePoint;
    public XRNode inputSource;

    public GameObject ammo;
    public GameObject electric;
    public GameObject electric2;
    public GameObject shield;

    public GameObject plasmaScreen;
    public GameObject lightningScreen;
    public GameObject shieldScreen;

    public Material normalMat;
    public Material selectedMat;

    public Material normalScreenMat;
    public Material selectedScreenMat;

    private int maxPlasmaAmmo = 10;
    private int maxElectricEnergy;

    private int plasmaAmmo;
    private int electricEnergy;

    private float nextPlasmaRecharge;
    private float nextEnergyRecharge;

    private bool primaryPressed = false;

    private float nextShot;
    private bool firingMissiles;

    private bool isElectric;
    private GameObject gm;
    private VRMapping controlls;

    private GameObject electricRange;
    private GameObject plasmaRange;

    // Start is called before the first frame update
    void Start()
    {
        plasmaAmmo = maxPlasmaAmmo;
        electricEnergy = maxElectricEnergy;
        gm = GameObject.Find("GameManager");
        controlls = gm.GetComponent<VRMapping>();
        electricRange = GameObject.Find("ElectricRange");
        plasmaRange = GameObject.Find("PlasmaRange");
        isElectric = false;
        plasmaRange.gameObject.SetActive(true);
        electricRange.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            
        }
        
        else
        {
            firingMissiles = false;
        }

        
        rechargeAmmo();
        ///////////////////////left primary///////////////////
        if ((controlls.leftPrimary || controlls.rightPrimary) && (controlls.leftTrigger < .2f || controlls.rightTrigger < .2f) || Input.GetButton("Fire2"))
        {
            if (!primaryPressed)
            {
                primaryPressed = true;
                if (isElectric)
                {
                    isElectric = false;
                    plasmaRange.gameObject.SetActive(true);
                    electricRange.gameObject.SetActive(false);
                    //setPlasma();
                }
                else
                {
                    isElectric = true;
                    plasmaRange.gameObject.SetActive(false);
                    electricRange.gameObject.SetActive(true);
                    //setElectric();


                }
            }

        }
        else
        {
            primaryPressed = false;
        }
        /////////////////////////////////////////////////////

        /////////////////////////right primary////////////////////
        if ((controlls.leftSecondary || controlls.rightSecondary || Input.GetButton("Fire3") || Input.GetButton("Jump")))
        {
            shield.SetActive(true);

            setShield();
        }
        else
        {
            shield.SetActive(false);
            if (isElectric)
            {
                setElectric();
            }
            else
            {
                setPlasma();
            }
        }
        /////////////////////////////////////////////////////////////


        if ((controlls.leftTrigger > .2f || controlls.rightTrigger > .2f) && !(controlls.leftSecondary || controlls.rightSecondary) || Input.GetButton("Fire1"))
        {
            if (isElectric)
            {
                electric.SetActive(true);
                electric.transform.position = leftFirePoint.transform.position;
                electric.transform.eulerAngles = leftFirePoint.transform.parent.eulerAngles;

                electric2.SetActive(true);
                electric2.transform.position = rightFirePoint.transform.position;
                electric2.transform.eulerAngles = rightFirePoint.transform.parent.eulerAngles;


            }
            else
            {
                if (Time.time > nextShot && plasmaAmmo > 0)
                {


                    Vector3 point = GetComponent<HeadTracking>().point;
                    var clone = Instantiate(ammo, leftFirePoint.transform.position, Quaternion.identity);
                    clone.GetComponent<Bullet>().direction = (point - leftFirePoint.transform.position).normalized;
                    nextShot = Time.time + 1;

                    var clone2 = Instantiate(ammo, rightFirePoint.transform.position, Quaternion.identity);
                    clone2.GetComponent<Bullet>().direction = (point - rightFirePoint.transform.position).normalized;
                    nextShot = Time.time + .3f;

                    Destroy(clone, 5);
                    Destroy(clone2, 5);
                    plasmaAmmo--;

                }
            }





        }
        else
        {
            electric.SetActive(false);
            electric2.SetActive(false);
        }

    }

    private void setElectric()
    {
        lightningScreen.GetComponent<Renderer>().material = selectedMat;
        plasmaScreen.GetComponent<Renderer>().material = normalMat;
        shieldScreen.GetComponent<Renderer>().material = normalMat;

        lightningScreen.transform.GetChild(0).GetComponent<Renderer>().material = selectedScreenMat;
        plasmaScreen.transform.GetChild(0).GetComponent<Renderer>().material = normalScreenMat;
        shieldScreen.transform.GetChild(0).GetComponent<Renderer>().material = normalScreenMat;

    }

    private void setPlasma()
    {
        lightningScreen.GetComponent<Renderer>().material = normalMat;
        plasmaScreen.GetComponent<Renderer>().material = selectedMat;
        shieldScreen.GetComponent<Renderer>().material = normalMat;

        lightningScreen.transform.GetChild(0).GetComponent<Renderer>().material = normalScreenMat;
        plasmaScreen.transform.GetChild(0).GetComponent<Renderer>().material = selectedScreenMat;
        shieldScreen.transform.GetChild(0).GetComponent<Renderer>().material = normalScreenMat;

    }

    private void setShield()
    {
        lightningScreen.GetComponent<Renderer>().material = normalMat;
        plasmaScreen.GetComponent<Renderer>().material = normalMat;
        shieldScreen.GetComponent<Renderer>().material = selectedMat;

        lightningScreen.transform.GetChild(0).GetComponent<Renderer>().material = normalScreenMat;
        plasmaScreen.transform.GetChild(0).GetComponent<Renderer>().material = normalScreenMat;
        shieldScreen.transform.GetChild(0).GetComponent<Renderer>().material = selectedScreenMat;

    }

    private void rechargeAmmo()
    {

        if (!firingMissiles)
        {
            if (Time.time > nextPlasmaRecharge)
            {
                if (plasmaAmmo < maxPlasmaAmmo)
                {
                    plasmaAmmo++;
                }
                nextPlasmaRecharge = Time.time + 2;
            }
        }
        else
        {

        }

        pAmmoDisplay.text = plasmaAmmo + "/ 10";
    }
}