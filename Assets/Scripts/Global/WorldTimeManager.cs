using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WorldTimeManager : MonoBehaviour
{
    public WorldTimeManager instance = null;

    [Header("当前时间状态")]
    [Range(0, 1)]
    public float time = 0f;

    public int days = 0;
    public float sec = 1440;

    public float hour;

    [Space]
    public bool stop = false;

    [Header("时间设定")]
    public float timeScale = 60;

    [Header("光照设定")]
    public Gradient gradient;

    [Header("光源")]
    public Light2D globalLight;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (stop) return;

        RecordTime();
        ReflashLightColor();
        ReflashMaterialColor();
    }

    /// <summary>
    /// 更新光源颜色及其他shader颜色
    /// </summary>
    private void ReflashLightColor() {
        globalLight.color = gradient.Evaluate(time);
        
    }

    private void ReflashMaterialColor() {
        ColorController.instance.SetColor(time);
    }

    private void RecordTime() {
        if (sec < 0)
        {
            sec = 1440.0f;
            days++;
        }
        else {
            sec -= Time.deltaTime * timeScale;
        }

        time = (1440f - sec) / 1440f;
        hour = time * 24;
    }
}
