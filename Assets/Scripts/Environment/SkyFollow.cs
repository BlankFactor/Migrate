﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyFollow : MonoBehaviour
{
    public Transform leaderBird;
    // Update is called once per frame
    void Update()
    {
        Vector2 v = leaderBird.position;
        v.y = transform.position.y;
        transform.position = v;
    }
}
