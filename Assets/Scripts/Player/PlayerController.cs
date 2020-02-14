using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("当前状态")]
    public bool speedUpPressed;
    public float birdHeight;
    public bool landed;
    public bool controllable;

    [Header("基本属性")]
    public float heightPointSpeed;
    
    [Header("飞行高度限制边界属性")]
    [Range(0,5)]
    public float border_Top;
    [Range(-5, 0)]
    public float border_Bottom;
    [Range(0,9)]
    public float border_Width;

    [Space]
    [Header("着陆地点检测区域属性")]
    public LayerMask landPosLayer;
    public float landPosCheckerOffset_X;
    public float landPosCheckerOffset_Y;
    public float landPosCheckerWidth;
    public float landPosCheckerHeight;

    [Header("对象及组件")]
    public LeaderBird bird;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        landed = true;
    }

    void Update()
    {
        if (bird == null) return;

        if (!landed)
        {
            BirdHeightControl();
            AccelerateBird();
            Land();
        }
        else {
            TakeOff();
        }
    }

    /// <summary>
    /// 调整鸟飞行高度
    /// </summary>
    private void BirdHeightControl() {
        if (!landed)
        {
            if (controllable)
            {
                birdHeight += Input.GetAxis("Mouse Y") * heightPointSpeed;
                birdHeight = Mathf.Clamp(birdHeight, border_Bottom, border_Top);
            }

            bird.ChangeHeight(birdHeight);
        }
    }

    /// <summary>
    /// 加速按键监测
    /// </summary>
    private void AccelerateBird() {
        if (Input.GetMouseButtonDown(1))
        {
            speedUpPressed = true;
            bird.SetSpeedUp(speedUpPressed);
        }
        else if (Input.GetMouseButtonUp(1)) {
            speedUpPressed = false;
            bird.SetSpeedUp(speedUpPressed);
        }
    }

    /// <summary>
    /// 着陆
    /// </summary>
    private void Land() {
        if (Input.GetMouseButtonDown(0)) {
            Collider2D[] cols;
            cols = Physics2D.OverlapBoxAll(new Vector2(bird.transform.position.x + landPosCheckerOffset_X,landPosCheckerOffset_Y), new Vector2(landPosCheckerWidth, landPosCheckerHeight),0,landPosLayer);
            if (cols.Length > 0) {
                bird.SetLanding(GetNearestLandPos(cols).GetComponent<LandPosData>());
                landed = true;
                CameraManager.instance.ConverToFarCamera();
            }
        }
    }

    private void TakeOff() {
        if (landed && bird.canTakeOff) {
            if (Input.GetMouseButtonDown(0)) {
                landed = false;
                bird.TakeOff();

                Invoke("GrandControl", 0.5f);
                CameraManager.instance.ConverToNearCamera();
            }
        }
    }

    /// <summary>
    /// 寻找最近的已探明着陆点
    /// </summary>
    /// <param name="_cols"></param>
    /// <returns></returns>
    private Collider2D GetNearestLandPos(Collider2D[] _cols) {
        float dis = 10000f;
        Collider2D col = null;

        foreach (var i in _cols) {
            float temp = Vector2.Distance(transform.position, i.transform.position);

            if (temp < dis) {
                col = i;
                dis = temp;
            }
        }
        return col;
    }

    private void GrandControl()
    {
        controllable = true;
    }

    public void SetBird(LeaderBird _bird) {
        bird = _bird;
    }
    public void ClearBird() {
        bird = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (bird == null) return;

        Gizmos.color = new Color(1.0f, 0f,0f, 1.0f);
        Gizmos.DrawLine(new Vector3(bird.transform.position.x - border_Width, border_Top, 0.0f), new Vector3(bird.transform.position.x + border_Width, border_Top, 0f));
        Gizmos.DrawLine(new Vector3(bird.transform.position.x - border_Width, border_Bottom, 0.0f), new Vector3(bird.transform.position.x + border_Width, border_Bottom, 0f));

        Gizmos.color = new Color(0.0f, 1f, 0f, 1.0f);
        Gizmos.DrawSphere(new Vector3(bird.transform.position.x, birdHeight, 0), 0.2f);

        Gizmos.color = new Color(0.0f, 0f, 1f, 0.5f);
        Gizmos.DrawCube(new Vector3(bird.transform.position.x + landPosCheckerOffset_X, landPosCheckerOffset_Y, 0), new Vector3(landPosCheckerWidth, landPosCheckerHeight,0));

    }
}
