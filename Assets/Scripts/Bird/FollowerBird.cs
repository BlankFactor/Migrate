using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBird : BBird
{
    [Header("当前飞行状态")]
    public Vector2 clusterPos;
    public bool inBorder;

    [Header("飞行设定")]
    [Range(0,2)]
    public float reflectionTime;
    public LeaderBird leader;
    
    public override void Start()
    {
        base.Start();
        leader.AddFollower(this);
    }

    public override void BirdDead()
    {
        leader.RemoveFollower(this);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void ChangeHeight(float _posY)
    {
        if (!transform.position.y.Equals(_posY + clusterPos.y))
        {
            Vector2 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, _posY + clusterPos.y, lerpSpeed);
            transform.position = pos;
        }
    }

    public void Command_TakeOff()
    {
        float time = Random.Range(0, reflectionTime);
        Invoke("TakeOff",time);

        clusterPos = leader.GetClusterPos();
    }

    public void Command_Land(Vector2 _pos) {
        float time = Random.Range(0, reflectionTime);
        SetLanding(_pos);
        Invoke("GrandLand", time);
    }

    public override void SetLanding(Vector2 _pos)
    {
        landPos = _pos;
    }

    private void GrandLand() {
        isFlying = false;
        isLanding = true;
    }

    private void SetInBorder(bool _v) {
        inBorder = _v;
    }
}
