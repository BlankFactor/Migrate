using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyFollow : MonoBehaviour
{
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        Vector3 v = target.position;
        v.y = transform.position.y;
        v.z = transform.position.z;
        transform.position = v;
    }
}
