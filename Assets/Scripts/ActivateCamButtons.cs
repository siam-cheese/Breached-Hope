using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCamButtons : MonoBehaviour
{
    public List<GameObject> connectedDoors;

    CentralDoorController doorScript;
    public GameObject CentralDoorObject;

    void Start()
    {

        CentralDoorObject = GameObject.Find("Door Controller");
        doorScript = CentralDoorObject.GetComponent<CentralDoorController>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    
    public void activateDoorButtons()
    {
        foreach (GameObject door in connectedDoors)
        {
            doorScript.enableButton(door);
        }
    }
}
