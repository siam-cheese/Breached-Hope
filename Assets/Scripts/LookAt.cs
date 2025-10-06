using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    public GameObject lookTarget;

    public bool LockX = false, LockY = false, LockZ = false;

    private float xRot, yRot, zRot;
    // Start is called before the first frame update
    void Start()
    {
        lookTarget = GameObject.Find("Player");
        xRot = transform.localEulerAngles.x;
        yRot = transform.localEulerAngles.y;
        zRot = transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookTarget.transform.position);
        if (LockX)
            transform.localEulerAngles = new Vector3(xRot, transform.localEulerAngles.y, transform.localEulerAngles.z);
        if (LockY)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRot, transform.localEulerAngles.z);
        if (LockZ)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, zRot);
    }
}
