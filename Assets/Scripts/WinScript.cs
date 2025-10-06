using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{

    Collider triggerArea;
    // Start is called before the first frame update
    void Start()
    {
        triggerArea = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        SceneManager.LoadScene("LPW_Demo");
    }
}
