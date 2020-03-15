using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Firefly : MonoBehaviour,IObserver
{
    [Header("浮动设置")]
    public bool random;
    [Space]
    public Vector2 margin = Vector2.one;
    public Vector2 frequency = Vector2.one;
    [Space]
    public float intensity = 6;
    public Gradient color;

    private float tick = 0;
    private Vector2 origin;
    private bool readyToDestory;
    private SpriteRenderer sprite;
    private Light2D light2d;

    delegate void delegate_CheckLifetime();
    delegate_CheckLifetime CheckLifetime;

    void Start()
    {
        ColorController.instance.AddObserver(this);

        origin = transform.position;
        sprite = GetComponent<SpriteRenderer>();

        light2d = GetComponent<Light2D>();

        tick = Random.Range(0f, 2f * Mathf.PI);

        if (random == true) {
            float s = Random.Range(0.3f, 1.5f);
            Vector2 sca = new Vector2(s, s);
            transform.localScale = sca;

            margin = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            frequency = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f));
        }
    }

    void Update()
    {
        Float();
    }

    void Float() {
        Vector2 v2 = origin;
        tick += Time.deltaTime;
        v2.x = origin.x + margin.x * Mathf.Sin(tick * frequency.x);
        v2.y = origin.y + margin.y * Mathf.Cos(tick * frequency.y);

        transform.position = v2;
    }

    public void Respond(float _v)
    {
        float a = 1 - color.Evaluate(_v).a;
        light2d.intensity = a * intensity;

        Color c = sprite.color;
        c.a = a;
        sprite.color = c;
    }

    public void DestoryObject() {
        Destroy(gameObject);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
