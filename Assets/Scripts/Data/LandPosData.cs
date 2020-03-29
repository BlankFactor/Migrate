using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandPosData : MonoBehaviour
{
    [Header("降落地点属性")]
    [Range(-1, 1)]
    public float offsetY = 0;
    [Range(-1, 1)]
    public float offsetX = 0;
    [Range(-3,3)]
    public float radius = 1;

    [System.Serializable]
    public struct eventStruct {
        public EventSpawner.eventName eventName;    
        public bool certain;
        [Range(0, 100)]
        public float probability;

        public Vector2 range;

        public bool fixedTime;
        [Range(0, 1)]
        public float fixedTimePoint;
    }
    [Header("事件配置列表")]
    public List<eventStruct> eventList = new List<eventStruct>();
    private Dictionary<EventSpawner.eventName, float> timeline = new Dictionary<EventSpawner.eventName, float>();

    [Header("休息处设定")]
    public bool energy;
    public bool healthPoint;

    public void DeployTimeline() {
        timeline.Clear();

        for (int i = 0; i < eventList.Count; i++) {
            if (eventList[i].certain)
            {
                if (eventList[i].fixedTime)
                {
                    timeline.Add(eventList[i].eventName, eventList[i].fixedTimePoint);
                }
                else
                {
                    float time = Random.Range(eventList[i].range.x, eventList[i].range.y);
                    time = Mathf.Clamp(time, 0, 1f);
                    timeline.Add(eventList[i].eventName, time);
                }
            }
            else {
                if (Random.Range(0, 1f) <= eventList[i].probability) {
                    if (eventList[i].fixedTime)
                    {
                        timeline.Add(eventList[i].eventName, eventList[i].fixedTimePoint);
                    }
                    else
                    {
                        float time = Random.Range(eventList[i].range.x, eventList[i].range.y);
                        time = Mathf.Clamp(time, 0, 1f);
                        timeline.Add(eventList[i].eventName, time);
                    }
                }
            }
        }

        GUIController.instance.SetTimeline(timeline);
    }

    public Vector2 GetLandPos() {
        Vector2 leftPoint = transform.position;
        Vector2 rightPoint = transform.position;

        leftPoint.x = leftPoint.x - radius + offsetX;
        leftPoint.y += offsetY;

        rightPoint.x = rightPoint.x + radius + offsetX;
        rightPoint.y += offsetY;

        Vector2 pos;
        pos.x = Random.Range(leftPoint.x, rightPoint.x);
        pos.y = leftPoint.y;

        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 leftPoint = transform.position;
        Vector2 rightPoint = transform.position;

        leftPoint.x = leftPoint.x - radius + offsetX;
        leftPoint.y += offsetY;

        rightPoint.x = rightPoint.x + radius + offsetX;
        rightPoint.y += offsetY;

        Gizmos.DrawLine(leftPoint, rightPoint);
    }

    /// <summary>
    /// 激活休息处动作
    /// </summary>
    /// <param name="bird"></param>
    public void Action(BBird bird) {
        if (energy) {
            bird.SetRestoreEnergy(true);
        }
        if (healthPoint) {
            bird.SetRestoreHP(true);
        }
    }
}
