using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBird : MonoBehaviour
{
    [Header("当前状态")]
    public float cur_Speed;
    public float cur_Energy;
    public float cur_healthPoint;

    [SerializeField]
    public bool isFlying = false;
    public bool isSpeedingUp = false;
    public bool isLanding = false;
    public bool landed = true;
    public bool canTakeOff = true;

    public Vector2 landPos;

    [Header("基本属性")]
    public float energy;
    public float maxSpeed;
    public float acceleration;
    public float healthPoint;
    [Range(0,0.1f)]
    public float lerpSpeed;
    public float landSpeed;

    [Space]
    public float dec_Energy_Normal;
    public float dec_Energy_SpeedUp;
    public float dec_HelathPoint;
    public float inc_Energy;
    public float inc_HealthPoint;
    public float inc_Speed;

    [Header("组件及对象")]
    public Rigidbody2D rid;

    public virtual void Start()
    {
        rid = GetComponent<Rigidbody2D>();

        cur_Energy = energy;
        cur_healthPoint = healthPoint;
    }


    public virtual void Update()
    {
        CheckEnergy();    
    }

    public virtual void FixedUpdate()
    {
        Fly_Horitionzal();
        Land();
    }

    /// <summary>
    /// 横向飞行
    /// </summary>
    private void Fly_Horitionzal() {
        if (isFlying)
        {
            Vector2 vel = rid.velocity;
            vel.x += acceleration * Time.fixedDeltaTime;
            vel.x = Mathf.Clamp(vel.x, 0, maxSpeed);
            ChangeVelocity(vel);
        }
        else {
            Vector2 vel = rid.velocity;
            vel.x -= acceleration * Time.fixedDeltaTime;
            vel.x = Mathf.Clamp(vel.x, 0, maxSpeed);
            ChangeVelocity(vel);
        }
    }

    /// <summary>
    /// 修改物体高度 插值计算
    /// </summary>
    /// <param name="_posY">目标高度点</param>
    public void ChangeHeight(float _posY) {
        if (transform.position.y != _posY) {
            Vector2 pos = transform.position;
            pos.y = Mathf.Lerp(pos.y, _posY, lerpSpeed);
            transform.position = pos;
        }
    }

    /// <summary>
    /// 降落至目标地点
    /// </summary>
    public void Land() {
        if (isLanding)
        {
            if (transform.position.Equals(landPos))
            {
                landed = true;
                isLanding = false;
                canTakeOff = true;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, landPos, landSpeed * Time.fixedDeltaTime);
            }
        }
    }

    /// <summary>
    /// 起飞
    /// </summary>
    public void TakeOff() {
        isFlying = true;
        landed = false;
        canTakeOff = false;

        Vector2 vel = rid.velocity;
        vel = new Vector2(maxSpeed / 2, 0);
        ChangeVelocity(vel);
    }

    /// <summary>
    /// 检测体力
    /// </summary>
    private void CheckEnergy() {
        if (isFlying)
        {
            if (cur_Energy <= 0)
            {
                BirdDead();
            }
            else
            {
                if (isSpeedingUp)
                    cur_Energy -= Time.deltaTime * dec_Energy_SpeedUp;
                else
                    cur_Energy -= Time.deltaTime * dec_Energy_Normal;
            }
        }
        else {
            cur_Energy += Time.deltaTime * inc_Energy;
            cur_Energy = Mathf.Clamp(cur_Energy, 0, energy);
        }
    }

    public virtual void BirdDead() { }

    public void SetSpeedUp(bool _v) {
        isSpeedingUp = _v;
        if (isSpeedingUp)
            maxSpeed += inc_Speed;
        else
            maxSpeed -= inc_Speed;
    }
    public void SetFlying(bool _v) {
        isFlying = _v;
    }
    public void SetLanding(Vector2 _pos) {
        isFlying = false;
        isLanding = true;
        landPos = _pos;
    }

    private void ChangeVelocity(Vector2 _vel) {
        rid.velocity = _vel;
        cur_Speed = _vel.x;
    }
}
