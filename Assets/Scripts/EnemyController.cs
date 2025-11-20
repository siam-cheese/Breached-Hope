using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Monster;

    public List<GameObject> SpawnPoints;

    public GameObject spawnMarkersParent;

    public float speed;

    public float chasingSpeed;

    public float chaseSpeedFinal;
    public float speedFinal;

    public GameObject chaseSoundObj;
    AudioSource chaseSound;


    private RaycastHit hitInfo; 


    UnityEngine.AI.NavMeshAgent navAgent;

    void Start()
    {
        chaseSound = chaseSoundObj.GetComponent<AudioSource>();
        navAgent = Monster.GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(RunEverySecond());
        Transform[] allChildTransforms = spawnMarkersParent.GetComponentsInChildren<Transform>(true);
        foreach (Transform childTrans in allChildTransforms)
        {
            SpawnPoints.Add(childTrans.gameObject);
        }
        speedFinal = speed;
        chaseSpeedFinal = chasingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = Player.transform.position;
        Vector3 monsterPos = Monster.transform.position;

        
        Vector3 rayToPlayer = playerPos - monsterPos;
        float rayMagnitude = rayToPlayer.magnitude;
        rayToPlayer = rayToPlayer.normalized;
        Ray ray = new Ray(monsterPos, rayToPlayer);
        Physics.Raycast(ray, out hitInfo, rayMagnitude);

        if (hitInfo.collider.gameObject.tag != "Player")
        {
            Debug.DrawRay(ray.origin, ray.direction * rayMagnitude, Color.red);
            navAgent.speed = speedFinal;
            chaseSound.volume = Mathf.Lerp(chaseSound.volume, 0, .8f * Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * rayMagnitude, Color.green);
            chaseSound.volume = Mathf.Lerp(chaseSound.volume, .5f, .8f * Time.deltaTime);
            navAgent.destination = playerPos;
            navAgent.speed = chaseSpeedFinal;


        }
    }

    IEnumerator RunEverySecond()
    {
        while (true) // This creates an infinite loop
        {
            Vector3 playerPos = Player.transform.position;
            yield return new WaitForSeconds(10f); // Wait for 1 second
            //Monster.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playerPos;
            bool seesPlayer = false;
            bool unacceptableSpawn = true;
            int rInd = Random.Range(0, SpawnPoints.Count);
            while (unacceptableSpawn)
            {
                rInd = Random.Range(0, SpawnPoints.Count);
                unacceptableSpawn = (SpawnPoints[rInd].transform.position - playerPos).magnitude > 10;
            }
            navAgent.destination = SpawnPoints[rInd].transform.position;
    
        }
    }
}
