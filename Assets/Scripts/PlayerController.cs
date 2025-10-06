using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController character;
    public float walkSpeed, walkFOV, standFOV;

    public float sprintSpeed, sprintFOV;

    public int mouseSensitivity;

    public int maxLookAngle;

    public float gravity;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private float playerCamStartY;

    public float bobSpeed;
    public float bobAmount;
    public float bobTimer = 0.0f;

    public GameObject playerCamera;

    public bool GROUNDED;

    public GameObject Flashlight;

    public Camera CamStats;


    private Vector3 curVel = new Vector3(0f, 0f, 0f);

    public KeyCode sprintKey = KeyCode.LeftShift, jumpKey = KeyCode.Space, interactKey = KeyCode.E;

    public GameObject footStepSFX;

    float resistance;

    public float groundResistance, airResistance;

    public AudioSource stepSFX;

    float pi = 2 * Mathf.PI;

    public List<AudioClip> Footsteps;

    private bool frozen = false;

    private Quaternion FLQuat; //flashlight vector
    
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        CamStats = playerCamera.GetComponent<Camera>();
        playerCamStartY = playerCamera.transform.localPosition.y;
        stepSFX = footStepSFX.GetComponent<AudioSource>();

    }
    void Update()
    {
        

        
        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity * Time.timeScale;
        pitch -= mouseSensitivity * Input.GetAxis("Mouse Y") * Time.timeScale;
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

        if (!frozen) {
            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }
        
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 TargetVelocity = new Vector3(xInput, 0f, zInput);


        if (Input.GetKey(sprintKey)) {
            TargetVelocity = transform.TransformDirection(TargetVelocity) * sprintSpeed;
            curVel.x = Mathf.Lerp(curVel.x, TargetVelocity.x, 4f * Time.deltaTime * resistance);
            curVel.z = Mathf.Lerp(curVel.z, TargetVelocity.z, 4f * Time.deltaTime * resistance);
            if (TargetVelocity.x == 0 && TargetVelocity.z == 0) {
                CamStats.fieldOfView = Mathf.Lerp(CamStats.fieldOfView, standFOV, 4f * Time.deltaTime);
            }
            else {
                CamStats.fieldOfView = Mathf.Lerp(CamStats.fieldOfView, sprintFOV, 3f * Time.deltaTime);
                bobTimer += Time.deltaTime * (bobSpeed + sprintSpeed);
                if (bobTimer > pi) {
                    bobTimer -= pi;
                    if (GROUNDED)
                        playFootstepAudio();
                }
            }
        }
        else {
            TargetVelocity = transform.TransformDirection(TargetVelocity) * walkSpeed;
            curVel.x = Mathf.Lerp(curVel.x, TargetVelocity.x, 5f * Time.deltaTime * resistance);
            curVel.z = Mathf.Lerp(curVel.z, TargetVelocity.z, 5f * Time.deltaTime * resistance);
            if (TargetVelocity.x == 0 && TargetVelocity.z == 0) {
                CamStats.fieldOfView = Mathf.Lerp(CamStats.fieldOfView, standFOV, 4f * Time.deltaTime);
                
            }
            else {
                CamStats.fieldOfView = Mathf.Lerp(CamStats.fieldOfView, walkFOV, 5f * Time.deltaTime);
                bobTimer += Time.deltaTime * (bobSpeed + walkSpeed);
                    if (bobTimer > pi) {
                    bobTimer -= pi;
                    if(GROUNDED) {
                        playFootstepAudio();
                    }
                }
            }
            
        }
        
        

        Flashlight.transform.localEulerAngles = new Vector3(pitch, 0f, 0f);

        //Head Bob
        playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x,playerCamStartY + Mathf.Sin(bobTimer)*bobAmount,playerCamera.transform.localPosition.z);


        
        if (curVel.y <= 0) {
            RaycastHit groundRay;
            GROUNDED = Physics.Raycast(gameObject.transform.position, new Vector3(0, -1, 0), out groundRay, 1.4f);
        }
        Debug.DrawRay(gameObject.transform.position, new Vector3(0, -1, 0) * 1.4f, Color.yellow);
        //(hit.collider.gameObject.tag == "ground")
        if((GROUNDED == true))
        {
            curVel.y = 0f;
            resistance = groundResistance;
        }
        else {
            curVel.y -= gravity * Time.deltaTime;
            resistance = airResistance;
        }

        if (GROUNDED && Input.GetKeyDown(jumpKey) ) {
            curVel.y += 5;
            GROUNDED = false;
        }
        character.Move(curVel * Time.deltaTime);

        // flicking levers

        //Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward, * 5f, Color.yellow);
        if (Input.GetKeyDown(interactKey)) {
            RaycastHit interact;
            bool didHit = Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out interact, 5f);
            if (didHit && interact.collider.gameObject.tag == "Lever") {
                interact.collider.gameObject.GetComponent<LeverScript>().turnOn();
            }
        }

    }

    void FixedUpdate() {
        
        
    }

    void playFootstepAudio() {
        //Debug.Log("step");
        stepSFX.pitch = Random.Range(.9f,1.1f);
        stepSFX.clip = Footsteps[Random.Range(0,2)];
        stepSFX.Play();
    }

    public void lookAt(Vector3 lookTo) {
        Debug.Log("test");
        playerCamera.transform.LookAt(lookTo);
        frozen = true;
        Time.timeScale = 0f;
    }
}
