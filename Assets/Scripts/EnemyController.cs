using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    public List<GameObject> targetObjects;

    int targetNum = 0;

    public GameObject loseScreen;

    public float normalSpeed;

    public float chasingSpeed;

    public bool onlyTargetPlayer = false;

    GameObject Player;
    bool seesPLayer = false;

    public bool targetPlayer = false;

    public float searchTIme = 5;
    private float search;
    public bool looseSight = true;

    Animator animator;


    public AudioSource AlertSFX, CaughtSFX, AudioSFX;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed;
        loseScreen = GameObject.Find("/Player/Main Camera/LooseScreen");
        Player = GameObject.Find("/Player");
        AlertSFX = GameObject.Find("playerSeen").GetComponent<AudioSource>();
        CaughtSFX = GameObject.Find("playerCaught").GetComponent<AudioSource>();
        AudioSFX= GameObject.Find("Audio Source").GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 travelTo = targetObjects[targetNum].transform.position;
        if (
            Mathf.Abs(targetObjects[targetNum].transform.position.x - transform.position.x) < 1 && 
            Mathf.Abs(targetObjects[targetNum].transform.position.z - transform.position.z) < 1 &&
            !targetPlayer
        ) {
            targetNum += 1;
            targetNum %= (targetObjects.Count);
            travelTo = targetObjects[targetNum].transform.position;
        }
        if (targetPlayer) {
            targetNum = targetObjects.Count - 1;
            travelTo = Player.transform.position;
            

        }
        travelTo.y = transform.position.y;
        if (onlyTargetPlayer) {
            if (targetPlayer) {
                agent.destination = Player.transform.position;
            }
        }
        else {
            agent.destination = travelTo;
        }
        
    }

    void FixedUpdate() {
        Vector3 direction = gameObject.transform.forward;
        Vector3 pos = gameObject.transform.position;
        Debug.DrawRay(pos, direction * 5f, Color.yellow);
        RaycastHit hit;
        bool didHit = Physics.Raycast(pos, direction, out hit, 5f);
        if(didHit == true)
        {
            Collider hitCol = hit.collider;
            GameObject other = hitCol.gameObject;
            if(other.tag == "Player" )
            {
                CaughtSFX.Play();
                Player.GetComponent<PlayerController>().lookAt(transform.position);
                Cursor.visible = true;
                AudioSFX.Stop();
                loseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        Debug.DrawRay(pos, direction * 20f, Color.green);
        bool scan = Physics.Raycast(pos, direction, out hit, 20f);
        if(scan == true && !targetPlayer)
        {
            
            Collider hitCol = hit.collider;
            GameObject other = hitCol.gameObject;
            if(other.tag == "Player" )
            {
                AlertSFX.Play();
                targetPlayer = true;
                agent.speed = chasingSpeed;
            }
        }
        Debug.DrawRay(pos, Player.transform.position - transform.position, Color.red);
        RaycastHit seePlayer;

        bool agroPlayer = Physics.Raycast(pos, (Player.transform.position - transform.position).normalized , out seePlayer,  (Player.transform.position - transform.position).magnitude);
        if(agroPlayer == true && targetPlayer)
        {
            
            Collider seePlayerCol = seePlayer.collider;
            GameObject seePlayerother = seePlayerCol.gameObject;
            //Debug.Log(seePlayerother.transform.position);
            //Debug.Log(seePlayerother.tag);
            if(seePlayerother.tag == "Player")
            {
                //Debug.Log("sees player");
                search = searchTIme;
            }
            else if (search > -2) {
                search -= Time.deltaTime;
            }
        }
        
        if (search < 0 && looseSight) {
            targetPlayer = false;
        }
    }

    public void targPlayer() {
        animator.Play("Hold Attack");
    }

    public void targPlayer2() {
        targetPlayer = true;
        agent.speed = chasingSpeed;
        Debug.Log("Hiiii");
        AlertSFX.Play();
    }
}
