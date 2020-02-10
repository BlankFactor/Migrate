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
    public float speedScale = 1;
    public float acceleration;
    public float healthPoint;
    [Range(0,5f)]
    public float climbSpeed;
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
            vel.x += acceleration  * Time.deltaTime;
            vel.x = Mathf.Clamp(vel.x, 0, maxSpeed * speedScale);
            ChangeVelocity(vel);
        }
        else {
            Vector2 vel = rid.velocity;
            vel.x -= acceleration * Time.deltaTime;
            vel.x = Mathf.Clamp(vel.x, 0, maxSpeed* speedScale);
            ChangeVelocity(vel);
        }
    }

    /// <summary>
    /// 修改物体高度 插值计算
    /// </summary>
    /// <param name="_posY">目标高度点</param>
    public virtual void ChangeHeight(float _posY) {
        if (transform.position.y != _posY && isFlying) {
            Vector2 pos = transform.position;
            //pos.y = Mathf.Lerp(pos.y, _posY, lerpSpeed);
            pos.y = Mathf.MoveTowards(pos.y, _posY, Time.fixedDeltaTime * climbSpeed);
            transform.position = pos;
        }
    }

    /// <summary>
    /// 降落至目标地点
    /// </summary>
    public virtual void Land() {
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
    public virtual void TakeOff() {
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
        else if(landed){
            cur_Energy += Time.deltaTime * inc_Energy;
            cur_Energy = Mathf.Clamp(cur_Energy, 0, energy);
        }
    }

    public virtual void BirdDead() {
        isFlying = false;
        canTakeOff = false;

        rid.bodyType = RigidbodyType2D.Dynamic;
        rid.gravityScale = 0.8f;

        Destroy(gameObject, 5);
    }

    /// <summary>
    /// 设置速度缩放
    /// </summary>
    /// <param name="_scale"></param>
    public void SetSpeedScale(float _scale = 1.0f) {
        speedScale = _scale;
    }

    public virtual void SetSpeedUp(bool _v) {
        isSpeedingUp = _v;
        if (isSpeedingUp)
            maxSpeed += inc_Speed;
        else
            maxSpeed -= inc_Speed;
    }
    public void SetFlying(bool _v) {
        isFlying = _v;
    }
    public virtual void SetLanding(LandPosData _lp) {
        isFlying = false;
        isLanding = true;
        landPos = _lp.GetLandPos();
    }

    private void ChangeVelocity(Vector2 _vel) {
        rid.velocity = _vel;
        cur_Speed = _vel.x;
    }
}
