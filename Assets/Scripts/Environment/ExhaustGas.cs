using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustGas : MonoBehaviour
{
    private ParticleSystem ps;
    private PolygonCollider2D pc;
    [Header("触发区当前状态")]
    public bool isCreating;
    public bool isDisabling;

    [Space]
    public bool forever;
    public bool trig = true;

    [Space]
    public bool enable = true;
    public bool triggerComplete = false;

    public float timer_Life;
    public float timer_Start;

    [Header("触发区设定")]
    [Range(0.1f,15)]
    public float width;
    [Range(0, 30)]
    public float height;
    [Range(-1,1)]
    public float offset_X = 0;

    [Space]
    public float time_Life;
    public float time_Start;

    [Space]
    public float dec_HealthPoint;
    public float speed;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        pc = GetComponent<PolygonCollider2D>();
        timer_Life = time_Life;
        timer_Start = time_Start;

        if (forever) {
            Vector2[] points = pc.points;

            points[2].y = height;
            points[2].x = LeftBorder(points[2].y);

            points[3].y = height;
            points[3].x = RightBorder(points[2].y);

            pc.points = points;

            ps.Play();
        }
        else
        CreateTrigger();
    }

    private void Update()
    {
        if (forever) return;

        CreatingTrigger();
        DisablingTrigger();
        Timer_Life();
        Timer_Start();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2 topLeft,topRight;

        topLeft = topRight  = transform.position;

        topLeft.x = topLeft.x - width + offset_X;
        topLeft.y += height;

        topRight.x += width + offset_X;
        topRight.y += height;

        Gizmos.DrawLine(transform.position, topLeft);
        Gizmos.DrawLine(transform.position, topRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.color = Color.blue;
    }

    public void Timer_Life() {
        if (!enable || !triggerComplete) return;

        if (timer_Life < 0)
        {
            timer_Life = time_Life;
            DisableTrigger();
            enable = false;
        }
        else {
            timer_Life -= Time.deltaTime;
        }
    }
    public void Timer_Start()
    {
        if (enable || !triggerComplete) return;

        if (timer_Start < 0)
        {
            timer_Start = time_Start;
            CreateTrigger();
            enable = true;
        }
        else
        {
            timer_Start -= Time.deltaTime;
        }
    }

    public void CreateTrigger() {
        isCreating = true;
        triggerComplete = false;
        ps.Play();
        
    }
    public void DisableTrigger() {
        isDisabling = true;
        triggerComplete = false;
        ps.Stop();
    }

    void CreatingTrigger() {
        if (isCreating)
        {
            Vector2[] points = pc.points;

            points[2].y += Time.deltaTime * speed;
            points[2].x = LeftBorder(points[2].y);

            points[3].y += Time.deltaTime * speed;
            points[3].x = RightBorder(points[2].y);

            points[2].y = points[3].y = Mathf.Clamp(points[2].y, 0, height);

            pc.points = points;

            if (points[2].y.Equals(height))
            {
                isCreating = false;
                triggerComplete = true;
            }
        }
    }

    void DisablingTrigger() {
        if (isDisabling) {
            Vector2[] points = pc.points;

            points[1].y += Time.deltaTime * speed;
            points[1].x = LeftBorder(points[1].y);

            points[0].y += Time.deltaTime * speed;
            points[0].x = RightBorder(points[0].y);

            points[0].y = points[1].y = Mathf.Clamp(points[0].y, 0, height);

            pc.points = points;

            if (points[0].y.Equals(points[3].y))
            {
                isDisabling = false;
                triggerComplete = true;
                ResetTrigger();
            }
        }
    }

    void ResetTrigger() {
        Vector2[] points = pc.points;

        for (int i = 0; i < points.Length; i++) {
            points[i] = Vector2.zero;
        }

        pc.points = points;
    }

    private float LeftBorder(float _y) {
        float a = height / (-width + offset_X);
        return _y /a;
    }
    private float RightBorder(float _y)
    {
        float a = height / (width + offset_X);
        return _y / a;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!trig) return;

        if (collision.tag.Contains("Bird")) {
            collision.SendMessage("SetDec_HealthPoint", dec_HealthPoint);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!trig) return;

        if (collision.tag.Contains("Bird"))
        {
            collision.SendMessage("SetDec_HealthPoint", -dec_HealthPoint);
        }
    }
}
