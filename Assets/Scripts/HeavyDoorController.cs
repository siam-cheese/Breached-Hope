using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeavyDoorController : MonoBehaviour
{
    // Start is called before the first frame update


    public Animator animator;

    public bool open = true;

    public GameObject CentralDoorObject;

    CentralDoorController doorScript;

    void Start()
    {
        animator = transform.parent.gameObject.GetComponent<Animator>();
        CentralDoorObject = GameObject.Find("Door Controller");
        doorScript = CentralDoorObject.GetComponent<CentralDoorController>();
        doorScript.doors.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        if (!open)
        {
            openDoor();
        }
        else
        {
            closeDoor();
        }
    }

    public void openDoor()
    {
        if (!open) {
            animator.SetTrigger("open");
            open = true;
        }
        
    }

    public void closeDoor()
    {
        if (open)
        {
            doorScript.openAllClosedDoors();
            animator.SetTrigger("close");
            open = false;
        }
    }
    
    
}
