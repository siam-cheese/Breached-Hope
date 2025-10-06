using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContols : MonoBehaviour
{
    public KeyCode openKey = KeyCode.O;
    public int activeCameraNum;
    GameObject activeCamera;

    GameObject CameraCanvas;

    public List<GameObject> Cameras;

    public GameObject playerCamera;

    public GameObject physicalCamera;

    PlayerController playerController;

    public int mouseSensitivity = 4;

    public int maxLookAngle = 90;

    bool camsOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        CameraCanvas = GameObject.Find("Camera Controller");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            if (!camsOpen)
            {
                playerController.enabled = false;
                camsOpen = true;
                playerCamera.SetActive(false);
                physicalCamera = Cameras[activeCameraNum].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                activeCamera = physicalCamera.transform.GetChild(0).GetChild(0).gameObject;
                activeCamera.SetActive(true);
                CameraCanvas.SetActive(true);
            }
            else
            {
                playerController.enabled = true;
                camsOpen = false;
                playerCamera.SetActive(true);
                activeCamera.SetActive(false);
                CameraCanvas.SetActive(false);
            }
        }

        if (camsOpen)
        {

            float pitch = physicalCamera.transform.GetChild(0).localEulerAngles.x;
            float yaw = physicalCamera.transform.localEulerAngles.z + Input.GetAxis("Mouse X") * mouseSensitivity * Time.timeScale;
            pitch -= mouseSensitivity * Input.GetAxis("Mouse Y") * Time.timeScale;
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
        physicalCamera = Cameras[activeCameraNum].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        activeCamera = physicalCamera.transform.GetChild(0).GetChild(0).gameObject;
        activeCamera.SetActive(true);
    }
}
