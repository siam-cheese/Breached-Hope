using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; // Required for UI element
using static KeyMaps;

public class CameraContols : MonoBehaviour
{
    
    public int activeCameraNum;
    GameObject activeCamera;

    public GameObject CameraCanvas;

    public List<GameObject> Cameras;

    public List<GameObject> ButtonCams;

    public GameObject playerCamera;

    public float cameraSensitivity;

    public GameObject physicalCamera;

    PlayerController playerController;

    public int mouseSensitivity = 4;

    public int maxLookAngle = 90;

    bool camsOpen = false;

    public Color offColor = Color.red;
    public Color onColor = Color.green;

    CentralDoorController doorScript;
    public GameObject CentralDoorObject;

    public GameObject Static;

    bool firstFrame = true;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        resetButtonColors();
        CentralDoorObject = GameObject.Find("Door Controller");
        doorScript = CentralDoorObject.GetComponent<CentralDoorController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstFrame)
        {
            FirstFrame();
        }
        if (Input.GetKeyDown(openKey))
        {
            if (!camsOpen)
            {
                playerController.enabled = false;
                camsOpen = true;
                playerCamera.SetActive(false);
                CameraCanvas.SetActive(true);
                physicalCamera = Cameras[activeCameraNum].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                activeCamera = physicalCamera.transform.GetChild(0).GetChild(0).gameObject;
                activeCamera.SetActive(true);

                Cameras[activeCameraNum].GetComponent<ActivateCamButtons>().activateDoorButtons();

                switchCam(Cameras[activeCameraNum]);
            }
            else
            {
                doorScript.hideAllCubes();
                playerController.enabled = true;
                camsOpen = false;
                playerCamera.SetActive(true);
                activeCamera.SetActive(false);
                CameraCanvas.SetActive(false);
            }
        }

    }

    void FixedUpdate()
    {
        if (camsOpen)
        {
            float yawMovement = 0, pitchMovement = 0;
            if (Input.GetKey(left)) yawMovement = -cameraSensitivity;
            else if (Input.GetKey(right)) yawMovement = cameraSensitivity;

            if (Input.GetKey(up)) pitchMovement = cameraSensitivity;
            else if (Input.GetKey(down)) pitchMovement = -cameraSensitivity;

            float pitch = physicalCamera.transform.GetChild(0).localEulerAngles.x;
            float yaw = physicalCamera.transform.localEulerAngles.z + yawMovement * mouseSensitivity * Time.timeScale;
            pitch -= mouseSensitivity * pitchMovement * Time.timeScale;
            //pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
            //yaw = Mathf.Clamp(yaw, -maxLookAngle, maxLookAngle);
            if (yaw < -maxLookAngle) yaw = -maxLookAngle;
            //else if(yaw > maxLookAngle) yaw = maxLookAngle;
            physicalCamera.transform.localEulerAngles = new Vector3(0, 0, yaw);
            physicalCamera.transform.GetChild(0).localEulerAngles = new Vector3(pitch, 0, 0);
        }
    }

    public void switchCam(GameObject cam)
    {
        activeCamera.SetActive(false);
        for (int i = 0; i < Cameras.Count; i++)
        {
            if (Cameras[i] == cam)
            {
                activeCameraNum = i;
                i = Cameras.Count;
            }
        }

        Static.GetComponent<Animator>().SetTrigger("playStatic");
        doorScript.hideAllCubes();
        Cameras[activeCameraNum].GetComponent<ActivateCamButtons>().activateDoorButtons();
        physicalCamera = Cameras[activeCameraNum].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        activeCamera = physicalCamera.transform.GetChild(0).GetChild(0).gameObject;
        activeCamera.SetActive(true);
        //Static.GetComponent<VideoPlayer>().camera = activeCamera;
        //Static.GetComponent<VideoPlayer>().Play();
        resetButtonColors();
    }

    public void resetButtonColors()
    {
        for (int i = 0; i < ButtonCams.Count; i++)
        {
            ButtonCams[i].GetComponent<Image>().color = offColor;
        }

        ButtonCams[activeCameraNum].GetComponent<Image>().color = onColor;
    }

    void FirstFrame()
    {
        firstFrame = false;
        doorScript.hideAllCubes();
    }
}
