using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject jumpscareObj; 
    public GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            jumpscareObj.GetComponent<AudioSource>().Play();
            Player.GetComponent<PlayerController>().frozen = true;
            Player.GetComponent<PlayerController>().lookAt(transform.position);
        }
    }
}
