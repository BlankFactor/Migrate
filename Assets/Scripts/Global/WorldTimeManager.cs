using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WorldTimeManager : MonoBehaviour
{
    public static WorldTimeManager instance;

    [Header("初始设定")]
    [Range(0,1)]
    public float initialTime = 0;

    [Header("当前时间状态")]
    [Range(0, 1)]
    public float time = 0f;

    public int days = 0;

    // 24h 共 1440s 60s/h 60缩放量实则 1s/h
    [Range(0,1440)]
    public float sec = 1440;

    public float hour;

    [Space]
    public bool stop = true;

    [Header("时间设定")]
    public float timeScale = 60;
    public float speedScale = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        days = 0;
        sec = 1440 * (1 - initialTime);
        time = (1440f - sec) / 1440f;
    }

    private void Update()
    {
        RecordTime();
        ReflashTime();
    }

    public void SetStop(bool _b) {
        stop = _b;
    }

    /// <summary>
    /// 更新光源颜色及其他shader颜色
    /// </summary>
    private void ReflashTime() {
        ColorController.instance.NotifyObserver(time);
        VolumeController.instance.NotifyObserver(time);
    }

    public float DeltaTime() {
        return Time.deltaTime * timeScale * speedScale;
    }

    private void RecordTime() {
        if (stop) return;

        if (sec < 0)
        {
            sec = 1440.0f;
            days++;
        }
        else {
            sec -= DeltaTime();
        }

        time = (1440f - sec) / 1440f;
        hour = time * 24;
    }

    /// <summary>
    /// 调整时间缩放 默认值为60
    /// </summary>
    /// <param name="_v">缩放量 缺省为60</param>
    public void SetTimeScale(float _v = 60f) {
        timeScale = _v;
    }

    public void SetSpeedScale(float _v) {
        speedScale = _v;
    }
}
