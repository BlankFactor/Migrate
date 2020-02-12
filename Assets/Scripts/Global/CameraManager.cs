using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public GameObject nearCamera;
    public GameObject farCamera;

    private void Awake()
    {
        instance = this;
    }
    public void ConverToNearCamera() {
        nearCamera.SetActive(true);
    }

    public void ConverToFarCamera() {
        nearCamera.SetActive(false);
    }
}
