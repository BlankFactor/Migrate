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

    [Header("休息处设定")]
    public bool energy;
    public bool healthPoint;

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
