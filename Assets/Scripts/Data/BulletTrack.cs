using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrack : MonoBehaviour
{
    public bool start;

    [Space]
    public float speed_pStart;
    public float speed_pEnd;

    public Vector2 pStart;
    public Vector2 pEnd;

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (!start) return;

        Action_pStart();
        Action_pEnd();
    }

    public void Initial(Vector2 _s,Vector2 _e)
    {
        pStart = _s;
        pEnd = _e;

        lr.SetPosition(0, _s);
        lr.SetPosition(1, _s);

        start = true;
    }

    void Action_pStart()
    {
        Vector2 p0 = lr.GetPosition(0);

        if(p0 != pEnd)
        {
            p0 = Vector2.MoveTowards(p0, pEnd, speed_pStart * Time.deltaTime);
            lr.SetPosition(0, p0);
        }
    }

    void Action_pEnd()
    {
        Vector2 p1 = lr.GetPosition(1);

        if (p1 != pEnd)
        {
            p1 = Vector2.MoveTowards(p1, pEnd, speed_pEnd * Time.deltaTime);
            lr.SetPosition(1, p1);
        }
        else {
            Destroy(gameObject);
        }
    }
}
