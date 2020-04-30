using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBird : MonoBehaviour
{
    [Header("当前状态")]
    public float cur_Speed;
    public float cur_Energy;
    public float cur_CoreEnergy;
    public float cur_Satiety;

    [Space]
    public bool alive = true;

    [SerializeField]
    public bool isFlying = false;
    public bool isSpeedingUp = false;
    public bool isLanding = false;
    public bool landed = true;
    public bool canTakeOff = true;

    [Space]
    public bool energy_Empty;

    public bool restoreEnergy;
    public bool restoreCoreEnergy;
    public bool hungry;

    public Vector2 landPos;
    public LandPosData lpd;

    [Header("基本属性配置")]
    public float energy;
    public float maxSpeed;
    public float speedScale = 1;
    private int speedScale_Animator = 1;
    public float acceleration;
    public float coreEnergy;
    public float satiety;
    [Range(0, 5f)]
    public float climbSpeed;
    public float landSpeed;

    [Space]
    public float dec_Energy_Normal;
    public float dec_Energy_SpeedUp;
    public float dec_CoreEnergy;
    public float dec_Satiety;
    public float inc_Energy = 1.0f;
    public float inc_CoreEnergy;
    public float inc_Speed;

    [Space]
    public float scale_RestoreEnergy = 1.0f;
    public float scale_RestoreCoreEnergy = 1.0f;
    

    [Header("组件及对象")]
    public Rigidbody2D rid;
    protected SpriteRenderer spriteRender;
    private Animator animator;
    private AudioSource audioSource;

    public virtual void Start()
    {
        rid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        cur_Energy = energy;
        cur_CoreEnergy = coreEnergy;
        cur_Satiety = satiety;
    }


    public virtual void Update()
    {
        if (!WorldTimeManager.instance.stop)
        {
            CheckEnergy();
            CheckCoreEnergy();
            CheckSatiety();
        }

        SwitchAnimation();
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
            vel.x += acceleration * Time.deltaTime;
            vel.x = Mathf.Clamp(vel.x, 0, maxSpeed * speedScale);
            ChangeVelocity(vel);
        }
        else {
            Vector2 vel = rid.velocity;
            vel.x -= acceleration * Time.deltaTime;
            vel.x = Mathf.Clamp(vel.x, 0, maxSpeed * speedScale);
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

                SetSpeedScale();

                //受事件处理
                canTakeOff = true;
                //lpd.Action(this);
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
        isLanding = false;
        canTakeOff = false;

        SetSpeedScale();
        lpd = null;
        SetRestoreEnergy(false);
        SetRestoreCoreEnergy(false);

        Vector2 vel = rid.velocity;
        vel = new Vector2(maxSpeed / 2, 0);
        ChangeVelocity(vel);
    }



    public virtual void Die(bool _sound = true) {
        isFlying = false;
        canTakeOff = false;

        alive = false;

        if(_sound)
            audioSource.Play();

        rid.bodyType = RigidbodyType2D.Dynamic;
        rid.gravityScale = 0.8f;

        Destroy(gameObject, 5);
    }

    private void SwitchAnimation() {
        animator.SetBool("Flying", isFlying);
        animator.SetBool("SpeedingUp", isSpeedingUp);
        animator.SetBool("Landing", isLanding);
        animator.SetInteger("SpeedScale", speedScale_Animator);
        animator.SetBool("Landed", landed);
    }

    /// <summary>
    /// 设置速度缩放
    /// </summary>
    /// <param name="_scale"></param>
    public void SetSpeedScale(float _scale = 1.0f) {
        speedScale = _scale;

        if (speedScale > 1)
            speedScale_Animator = 2;
        else if (speedScale < 1)
            speedScale_Animator = 0;
        else if(speedScale == 1.0f)
            speedScale_Animator = 1;
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
        lpd = _lp;

        // 重设速度缩放
        if(isSpeedingUp)
            SetSpeedUp(false);
        SetSpeedScale();
    }

    #region 体力
    /// <summary>
    /// 检测体力
    /// </summary>
    protected virtual void CheckEnergy()
    {
        if (isFlying)
        {
            if (cur_Energy <= 0)
            {
                //BirdDead(); 体力为空时消耗生命值而非立即死亡

                if (!energy_Empty)
                    energy_Empty = true;
            }
            else
            {
                if (isSpeedingUp)
                    cur_Energy -= Time.deltaTime * dec_Energy_SpeedUp;
                else
                    cur_Energy -= Time.deltaTime * dec_Energy_Normal;

                cur_Energy = Mathf.Clamp(cur_Energy, 0, cur_CoreEnergy);
            }
        }
        else if (landed && restoreEnergy)
        {
            if (energy_Empty)
            {
                energy_Empty = false;
            }

            cur_Energy += Time.deltaTime * inc_Energy * scale_RestoreEnergy;
            cur_Energy = Mathf.Clamp(cur_Energy, 0, cur_CoreEnergy);
            //cur_Energy = Mathf.Clamp(cur_Energy, 0, energy);
        }
    }


    public void SetRestoreEnergy(bool _v, float _scale = 1.0f)
    {
        restoreEnergy = _v;

        if (restoreEnergy)
        {
            if (scale_RestoreEnergy < _scale)
                scale_RestoreEnergy = _scale;
        }
        else
        {
            scale_RestoreEnergy = _scale;
        }
    }

    #endregion

    #region 核心体力
    /// <summary>
    /// 检测核心体力
    /// </summary>
    private void CheckCoreEnergy()
    {
        // 体力为空时消耗核心体力
        if (energy_Empty && isFlying)
            cur_CoreEnergy -= dec_CoreEnergy * Time.deltaTime * 5.0f;

        if (hungry)
        {
            cur_CoreEnergy -= dec_CoreEnergy * Time.deltaTime;
        }

        if (restoreCoreEnergy)
        {
            cur_CoreEnergy += inc_CoreEnergy * scale_RestoreCoreEnergy * Time.deltaTime;
            cur_CoreEnergy = Mathf.Clamp(cur_CoreEnergy, 0, coreEnergy);
        }

        if (cur_CoreEnergy <= 0)
        {
            if (hungry && alive)
                EventRecorder.instance.Add_Hungry(1);

            Die();
        }
    }
    /// <summary>
    /// 设置回复核心体力
    /// </summary>
    /// <param name="_v"></param>
    /// <param name="_scale">若 回复 体力 则第二个参数为回复速度缩放</param>
    public void SetRestoreCoreEnergy(bool _v, float _scale = 1.0f)
    {
        restoreCoreEnergy = _v;

        if (restoreCoreEnergy)
        {
            if(scale_RestoreCoreEnergy < _scale)
                scale_RestoreCoreEnergy = _scale;
        }
        else {
            scale_RestoreCoreEnergy = _scale;
        }
    }

    public void AddCoreEnergy(float _v) {
        cur_CoreEnergy += _v;
        cur_CoreEnergy = Mathf.Clamp(cur_CoreEnergy, 0, coreEnergy);
    }

    private void SetDec_CoreEnergy(float _v)
    {
        dec_CoreEnergy += _v;
    }

    #endregion

    #region 饱食度
    public virtual void CheckSatiety()
    {
        if (!hungry && GameManager.instance.gameStart)
        {
            cur_Satiety -= Time.deltaTime * dec_Satiety;
            cur_Satiety = Mathf.Clamp(cur_Satiety, 0, satiety);

            if (cur_Satiety == 0)
            {
                hungry = true;
            }
        }
    }

    /// <summary>
    /// 重设饱食度
    /// </summary>
    public virtual void ResetSatiety()
    {
        hungry = false;
        cur_Satiety = satiety;
    }


    /// <summary>
    /// 增加饱食度 
    /// </summary>
    /// <param name="_scale">增量系数 为参数值 * 最大值</param>
    public virtual void AddSatiety(float _scale)
    {
        cur_Satiety += satiety * _scale;
        cur_Satiety = Mathf.Clamp(cur_Satiety, 0, satiety);

        hungry = false;
    }
    #endregion


    protected void ChangeVelocity(Vector2 _vel) {
        rid.velocity = _vel;
        cur_Speed = _vel.x;
    }


    public float GetCurSpeed()
    {
        return cur_Speed;
    }


}
