using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> SpawnPoints;

    public GameObject spawnMarkersParent;

    public GameObject pointAudio;

    public int winCondition;

    public int score;

    void Start()
    {

        Transform[] allChildTransforms = spawnMarkersParent.GetComponentsInChildren<Transform>(true);
        foreach (Transform childTrans in allChildTransforms)
        {
            SpawnPoints.Add(childTrans.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        score++;
        bool unacceptableSpawn = true;
        int rInd = Random.Range(0, SpawnPoints.Count);
        while (unacceptableSpawn)
        {
            rInd = Random.Range(0, SpawnPoints.Count);
            unacceptableSpawn = (SpawnPoints[rInd].transform.position - transform.position).magnitude < 20;
        }
        transform.position = SpawnPoints[rInd].transform.position;
        pointAudio.GetComponent<AudioSource>().Play();
    }
}
