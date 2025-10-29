using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CentralDoorController : MonoBehaviour
{
    public List<GameObject> doors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openAllClosedDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].GetComponent<HeavyDoorController>().openDoor();
        }
    }

    public void hideAllCubes()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].GetComponent<MeshRenderer>().enabled = false;
            doors[i].GetComponent<BoxCollider>().enabled = false;
        }
    }
    
    public void enableButton(GameObject door)
    {
        door.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;
        door.transform.GetChild(1).gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
