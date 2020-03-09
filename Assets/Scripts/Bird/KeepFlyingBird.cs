using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepFlyingBird : BBird
{
    [Space]
    public SpriteRenderer sprite;

    delegate void reflashStatu();
    reflashStatu ReflashStatu;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ReflashStatu = ShowUp;
    }

    // 初始化大小及速度
    public void Initial(float _speed,float _scale) {
        // 随机化设置
        maxSpeed = _speed;
        Vector2 scalev2 = new Vector2(_scale, _scale);
        sprite.transform.localScale = scalev2;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        ReflashStatu();
    }

    private void ShowUp()
    {
        if (sprite.isVisible.Equals(true))
        {
            ReflashStatu = ReadyToDestory;
        }
    }
    private void ReadyToDestory()
    {
        if (!sprite.isVisible.Equals(true))
            Destroy(gameObject, 2f);
    }
}
