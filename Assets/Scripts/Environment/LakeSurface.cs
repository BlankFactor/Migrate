using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LakeSurface : MonoBehaviour
{
    public string softLayer;

    private MeshRenderer mr;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mr.sortingLayerName = softLayer;
    }
}
