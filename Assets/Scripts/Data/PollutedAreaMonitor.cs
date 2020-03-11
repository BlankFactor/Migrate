using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

public class PollutedAreaMonitor : MonoBehaviour
{
    public Volume saturation;
    public ColorSetter skyGradient;

    [Header("设置属性")]
    public float minDistance;
    public float maxDistance;

    [Header("变化缩放")]
    public float scale_SkyAlpha = 1.5f;
    public float scale_Saturation = 1.0f;

    private float distance;
    [Space]
    public Transform target;

    [DisplayOnly]
    public bool inArea;
    private bool sendedStop;

    private void Start()
    {
        distance = maxDistance - minDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Monitor();
    }

    void Monitor() {
        float dis = Mathf.Abs(transform.position.x - target.position.x);
        inArea = dis > maxDistance ? false : true;

        if (inArea)
        {
            //停止曝光
            if (dis <= maxDistance)
            {
                sendedStop = true;
                VolumeController.instance.SetStop(true);
            }
            else if (dis > maxDistance && sendedStop)
            {
                VolumeController.instance.SetStop(false);
                sendedStop = false;
            }

            dis -= minDistance;
            // 饱和度修改
            dis = Mathf.Clamp(dis, 0, distance);

            float v = (distance - dis) / distance;
            saturation.weight = v;

            // 星空Alpha值修改
            float v2 = (distance - dis) / distance;
            v2 *= scale_SkyAlpha;
            v2 = Mathf.Clamp(v2, 0, 1);

            GradientAlphaKey[] gak = skyGradient.color.alphaKeys;
            gak[0].alpha = v2;
            gak[3].alpha = v2;
            skyGradient.color.alphaKeys = gak;
        }
        else if (!inArea && sendedStop) {
            VolumeController.instance.SetStop(false);
            sendedStop = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 min = transform.localPosition;
        Vector3 max = transform.localPosition;

        Vector3 min2 = min;
        Vector3 max2 = max;

        min.x += minDistance;
        max.x += maxDistance;

        min2.x -= minDistance;
        max2.x -= maxDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(max, max2);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(min, min2);
    }
}

public class DisplayOnly : PropertyAttribute
{

}
[CustomPropertyDrawer(typeof(DisplayOnly))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
