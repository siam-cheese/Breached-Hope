using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorAudioScript : MonoBehaviour
{
    public GameObject doorOpen;
    public GameObject doorClose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openAudio()
    {
        doorOpen.GetComponent<AudioSource>().Play();
    }

    public void closeAudio()
    {
        doorClose.GetComponent<AudioSource>().Play();
    }
}
