using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public float controllerRotationSpeed;
    public bool usingHotas;
    public float speed;
    public GameObject xrRig;

    private XRRig rig;
    private CharacterController character;

    private GameObject gm;
    private VRMapping controlls;

    private bool grounded;
    public GameObject groundCheck;
    public LayerMask mask;

    private AudioSource audio;
    private bool isWalking;
    private Animator anim;

    private float nextPauseTime;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        controlls = gm.GetComponent<VRMapping>();
        character = GameObject.Find("Player").GetComponent<CharacterController>();
        rig = xrRig.GetComponent<XRRig>();
        audio = GetComponent<AudioSource>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        //Vector3 direction = headYaw * new Vector3(controlls.leftStick.x, 0, controlls.leftStick.y);
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        
        if (controlls.leftStick.y > .5 || controlls.leftStick.y < -.5 || Input.GetAxis("Throttle") > .5f || Input.GetAxis("Throttle") < -.5f || Input.GetAxis("Vertical") > .5f || Input.GetAxis("Vertical") < -.5f)
        {
            Debug.Log("walking");
            if (!isWalking)
            {
                anim.Play("MechWalk");
                anim.SetBool("isWalking", true);
                
                isWalking = true;                
                
            }
            if (Time.time > nextPauseTime)
            {
                StartCoroutine(Pause());

            }
            if (!isPaused)
            {
                if (Input.GetAxis("Throttle") > .5f || Input.GetAxis("Throttle") < -.5f)
                {
                    character.Move(direction * Time.fixedDeltaTime * speed * Input.GetAxis("Throttle"));
                    Debug.Log(Input.GetAxis("Throttle"));
                }
                else
                {
                    if(Input.GetAxis("Vertical") > .5f || Input.GetAxis("Vertical") < -.5f)
                    {
                        character.Move(direction * Time.fixedDeltaTime * speed * Input.GetAxis("Vertical"));
                    }
                    else if(controlls.leftStick.y > .5f || controlls.leftStick.y < .5f)
                    {
                        character.Move(direction * Time.fixedDeltaTime * speed * controlls.leftStick.y);
                    }
                    
                }
                
            }
            

        }
        else
        {
            anim.SetBool("isWalking", false);
            audio.Stop();
            isWalking = false;
        }

        if (controlls.rightStick.x > .5f || Input.GetAxis("Twist") < -.5f || Input.GetAxis("Horizontal") > .5f)
        {
            character.transform.Rotate(0, controllerRotationSpeed, 0);
        }
        if (controlls.rightStick.x < -.5f || Input.GetAxis("Twist") > .5f || Input.GetAxis("Horizontal") < -.5f)
        {
            character.transform.Rotate(0, -controllerRotationSpeed, 0);
        }


        if (!grounded)
        {
            transform.position += new Vector3(0, -1f, 0) * Time.deltaTime;
        }

        if (Physics.CheckSphere(groundCheck.transform.position, .5f, mask))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }


    }



    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    IEnumerator Pause()
    {
        nextPauseTime = Time.time + 1;
        isPaused = false;
        audio.Play();
        yield return new WaitForSeconds(.85f);
        isPaused = true;

    }



}

