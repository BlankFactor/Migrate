using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    public float mugnitude;
    public float frequency;

    private Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v3 = pos;
        v3.y += mugnitude * Mathf.Sin(frequency * Time.time);

        transform.position = v3;
    }
}
