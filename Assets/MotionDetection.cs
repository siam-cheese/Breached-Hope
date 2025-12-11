using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionDetection : MonoBehaviour
{
    public GameObject cameraflash;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ENEMY")
            cameraflash.GetComponent<Animator>().SetTrigger("MotionDetected");
    }
}
