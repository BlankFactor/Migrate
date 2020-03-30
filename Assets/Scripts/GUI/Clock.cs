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

    private bool clickable;

    public List<BEvent> cur_EventList = new List<BEvent>();

    bool check;
    EventSpawner.eventName cur_Event;

    [Header("对象 ")]
    public LeaderBird leaderBird;


    public Sprite ss;
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

                GUIController.instance.Display_Panel_EventDesc(cur_EventList[cur_EventList.Count - 1].GetIllu(), cur_EventList[cur_EventList.Count - 1].GetDesc());

                Debug.Log(cur_Event);
            }

            if (cur_Time.Equals(target_Time))
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        WorldTimeManager.instance.SetStop(true);
        stop = true;

        clickable = false;

        WorldTimeManager.instance.SetSpeedScale(speedScaleWhenClocking);
    }

    private void OnDisable()
    {
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

        clickable = false;
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
