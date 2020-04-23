using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public GameObject nearCamera;
    public GameObject farCamera;
    public GameObject tempCamera;
    public GameObject telephotoCamera;

    public SkyFollow sky;

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

    public void ConverToTelephotoCamera()
    {
        telephotoCamera.GetComponent<CinemachineVirtualCamera>().Priority = 50;
        sky.target = telephotoCamera.transform;
    }

    public void RemoveTempCamera() {
        tempCamera.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        
    }


    public void ClearFollowTarget() {
        nearCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;
        farCamera.GetComponent<CinemachineVirtualCamera>().Follow = null;
        ConverToFarCamera();
    }
}
