using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> TargetObj;
    public GameObject parent;

    private Animator animator;

    private bool flipped = false;

    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOn(){
        if (!flipped) {
            animator.Play("PullLever");
            flipped = true;
        }
    }

    public void openDoors() {
        Debug.Log("hit switch!");
            for (int i = 0; i < TargetObj.Count; i++) {
                if (TargetObj[i].tag == "RUN")
                    TargetObj[i].SetActive(true);
                if (TargetObj[i].tag == "Door")
                    TargetObj[i].GetComponent<leverLink>().leverTrigger();
                if (TargetObj[i].tag == "Enemy")
                    TargetObj[i].GetComponent<EnemyController>().targPlayer();
                
            }
    }
}
