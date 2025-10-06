using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverLink : MonoBehaviour
{
    private Animator animator;
    public GameObject audioController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void leverTrigger() {
        audioController.GetComponent<AudioSource>().Play();
        animator.Play("DoorOpen");
    }
}
