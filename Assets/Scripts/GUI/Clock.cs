using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private Animator animator;
    private Dictionary<EventSpawner.eventName, float> timeline = new Dictionary<EventSpawner.eventName, float>();

    [Header("时钟状态")]
    public float cur_Time;
    private float target_Time;
    [Space]
    public float speedScaleWhenClocking = 1;

    private bool stop = true;
    [SerializeField]
    private bool click;
    private bool clickable;

    public List<BEvent> cur_EventList = new List<BEvent>();

    private float initialTime;

    bool check;
    EventSpawner.eventName cur_Event;

    [Header("对象 ")]
    public LeaderBird leaderBird;
    public RectTransform rectTrans;
    public RectTransform rectTrans_Outline;
    private float maxAngle = 360f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 监听输入
        if (clickable) {
            if (Input.GetMouseButtonDown(0)) {
                clickable = false;

                WorldTimeManager.instance.SetStop(false);
                stop = false;

                GUIController.instance.Disable_Panel_EventDesc();
            }
        }

        if (stop) return;

        else
        {
            cur_Time += WorldTimeManager.instance.DeltaTime() / 1440f;
            cur_Time = Mathf.Clamp(cur_Time, 0, target_Time);

            // 检查事件是否被触发
            foreach (var v in timeline)
            {
                if (cur_Time >= v.Value)
                {
                    check = true;
                    cur_Event = v.Key;
                    break;
                }
            }
            // 触发事件
            if (check)
            {
                check = false;
                clickable = true;

                timeline.Remove(cur_Event);

                BEvent ev = EventSpawner.GetEvent(cur_Event);
                ev.Execute(leaderBird);
                cur_EventList.Add(ev);

                WorldTimeManager.instance.SetStop(true);
                stop = true;

                Debug.Log(cur_Event);
            }

            if (cur_Time.Equals(target_Time))
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ReflashEularAngles(float _v) {
        rectTrans.localEulerAngles = new Vector3(0, 0, (_v + 0.45f) * maxAngle);
        rectTrans_Outline.localEulerAngles = new Vector3(0, 0, (_v - initialTime) * maxAngle);
    }

    private void OnEnable()
    {
        WorldTimeManager.instance.SetStop(true);
        stop = true;

        clickable = false;

        WorldTimeManager.instance.SetSpeedScale(speedScaleWhenClocking);

        GUIController.instance.AddString("选择在此处停留的时间,停留的越久,也许发现的更多");

        initialTime = WorldTimeManager.instance.time;
        rectTrans_Outline.localEulerAngles = Vector3.zero;

        PlayerController.instance.SetCursor(true);
    }

    private void OnDisable()
    {
        initialTime = 0;
        rectTrans_Outline.localEulerAngles = Vector3.zero;

        WorldTimeManager.instance.SetStop(true);
        stop = true;

        leaderBird.GrantToTakeOff();

        // 时钟归位
        animator.SetBool("Minify", false);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        GetComponent<RectTransform>().localScale = Vector2.one;

        // 清空事件及其效果
        ClearEventList();

        WorldTimeManager.instance.SetSpeedScale(1);
        GUIController.instance.Disable_Panel_EventDesc();

        clickable = false;

        click = false;
        PlayerController.instance.SetCursor(false);
    }

    // 传递时间线
    public void SetTimeline(object _timeline) {
        ClearTimeline();
        timeline = _timeline as Dictionary<EventSpawner.eventName, float>;
        foreach (var v in timeline)
        {
            Debug.Log(v.ToString());
        }

    }

    // 设置停留时间并开始计时
    public void SetTime(float _value) {
        if (click) return;

        click = true;

        target_Time = _value;

        animator.SetBool("Minify", true);

        stop = false;
        WorldTimeManager.instance.SetStop(false);
    }

    public void ClearTimeline() {
        timeline.Clear();
        cur_Time = 0;
    }

    public void ClearEventList()
    {
        foreach (var v in cur_EventList)
            v.Undo(leaderBird);

        cur_EventList.Clear();
    }
}
