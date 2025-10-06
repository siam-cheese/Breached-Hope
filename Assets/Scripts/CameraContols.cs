using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContols : MonoBehaviour
{
    public KeyCode openKey = KeyCode.O;
    public int activeCameraNum;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(openKey)) {
            if (!camsOpen) {
                playerController.enabled=false;
                camsOpen = true;
                playerCamera.SetActive(false);
                physicalCamera = Cameras[activeCameraNum].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
                Cameras[activeCameraNum].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            else {
                playerController.enabled=true;
                camsOpen = false;
                playerCamera.SetActive(true);
                Cameras[activeCameraNum].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
        }

        if (camsOpen) {
            
            float pitch = physicalCamera.transform.localEulerAngles.x;
            float yaw = physicalCamera.transform.localEulerAngles.z + Input.GetAxis("Mouse X") * mouseSensitivity * Time.timeScale;
            pitch -= mouseSensitivity * Input.GetAxis("Mouse Y") * Time.timeScale;
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
            //yaw = Mathf.Clamp(yaw, -maxLookAngle, maxLookAngle);
            if(yaw < -maxLookAngle) yaw = -maxLookAngle;
            //else if(yaw > maxLookAngle) yaw = maxLookAngle;
            physicalCamera.transform.localEulerAngles = new Vector3(0, 0, 0);
            physicalCamera.transform.localEulerAngles = new Vector3(0, 0, yaw);
        }

    }
}
