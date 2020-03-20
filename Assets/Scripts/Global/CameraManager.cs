﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public GameObject nearCamera;
    public GameObject farCamera;

    private int priority;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        priority = farCamera.GetComponent<CinemachineVirtualCamera>().Priority;
    }

    public void ConverToNearCamera() {
        nearCamera.GetComponent<CinemachineVirtualCamera>().Priority = priority + 1;
    }

    public void ConverToFarCamera() {
        nearCamera.GetComponent<CinemachineVirtualCamera>().Priority = priority - 1;
    }

    public void ClearFollowTarget() {
        nearCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;
        farCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;
        ConverToFarCamera();
    }
}
