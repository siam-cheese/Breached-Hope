using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Monster;


    void Start()
    {
        StartCoroutine(RunEverySecond());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RunEverySecond()
    {
        while (true) // This creates an infinite loop
        {
            Vector3 playerPos = Player.transform.position;
            yield return new WaitForSeconds(20f); // Wait for 1 second
            Monster.GetComponent<UnityEngine.AI.NavMeshAgent>().destination = playerPos;
        }
    }
}
