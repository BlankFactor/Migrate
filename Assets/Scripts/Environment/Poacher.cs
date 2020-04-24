using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poacher : MonoBehaviour
{
    [Header("当前状态")]
    public Vector2 viewDir;
    public GameObject target;

    [Space]
    public bool searching = true;
    public bool findTarget;
    public bool lockTarget;
    public bool aiming;

    [Space]
    public float timer_LockTarget;
    public float timer_Aim;

    [Header("射击设定")]
    [Range(1,50)]
    public float radius;
    public float shootDistanceScale = 1;
    public Vector2 offset;

    [Space]
    [Range(0,1)]
    public float anticipationPrecision = 1.5f;
    [Range(0, 1)]
    public float aimLerp = 0.5f;

    [Space]
    public float time_Search;
    public float time_Aim;
    public float time_Shoot;
    [Space]
    public LayerMask birdLayer;

    [Space]
    private LineRenderer lr;
    public GameObject bulletTrack;
    public SpriteRenderer gun;
    public GameObject muzzleLight;

    [Header("枪支音频")]
    public AudioSource audio_Gunshot;
    public AudioClip sound_Gunshot;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position + new Vector3(offset.x, offset.y, 0));
        lr.SetPosition(1, transform.position + new Vector3(offset.x, offset.y, 0));

        timer_LockTarget = time_Search;
        timer_Aim = time_Aim;
    }

    void Update()
    {
        Search();
        LockTarget();
        Aim();
    }

    void Search() {
        if (!lockTarget && searching) {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position + new Vector3(offset.x, offset.y, 0), radius, birdLayer);

            if (cols.Length > 0)
            {
                findTarget = true;
            }
            else if(findTarget && cols.Length <= 0){
                findTarget = false;

                timer_LockTarget = time_Search;

                viewDir = Vector2.zero;
                target = null;
            }
        }
    }

    void LockTarget()
    {
        if (findTarget) {
            if (timer_LockTarget < 0)
            {
                findTarget = false;
                searching = false;

                lockTarget = true;
                aiming = true;

                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position + new Vector3(offset.x, offset.y, 0), radius, birdLayer);
                int index = Random.Range(0, cols.Length);

                target = cols[index].gameObject;

                timer_LockTarget = time_Search;
            }
            else {
                timer_LockTarget -= Time.deltaTime;
            }
        }
    }

    void Aim()
    {
        if(aiming && lockTarget)
        {
            if (timer_Aim <= 0)
            {
                aiming = false;

                Invoke("Shoot", time_Shoot);
            }
            else {
                Vector2 targetDir = target.transform.position - transform.position;
                targetDir.x += GetAnticipation(target.transform.position.y);

                viewDir = Vector2.Lerp(viewDir, targetDir, aimLerp);
                viewDir.Normalize();

                Vector3 gunAngle = gun.transform.eulerAngles;
                gunAngle.z = Vector2.Angle(Vector2.right, viewDir);
                gun.transform.eulerAngles = gunAngle;

                viewDir *= radius;

                timer_Aim -= Time.deltaTime;
            }

            Vector2 origin = transform.position + new Vector3(offset.x, offset.y, 0);
            lr.SetPosition(1, shootDistanceScale * viewDir + origin);
        }
    }

    void Shoot()
    {
        Vector2 origin = transform.position + new Vector3(offset.x, offset.y, 0);
        RaycastHit2D hit = Physics2D.Raycast(origin, viewDir, radius * shootDistanceScale, birdLayer);

        // 枪声
        Play_Gunshot();

        // 枪口光
        StartCoroutine("Display_MuzzleLight");

        if (hit)
        {
            hit.transform.SendMessage("Die",true);
            EventRecorder.instance.Add_Poarching(1);
        }

        GameObject bt = Instantiate(bulletTrack);
        bt.GetComponent<BulletTrack>().Initial(origin, origin + viewDir * shootDistanceScale);

        lr.SetPosition(1, transform.position + new Vector3(offset.x, offset.y, 0));

        target = null;
        viewDir = Vector2.zero;

        searching = true;
        aiming = false;
        findTarget = false;
        lockTarget = false;

        timer_Aim = time_Aim;
    }

    void Play_Gunshot() {
        audio_Gunshot.Play();
    }

    IEnumerator Display_MuzzleLight() {
        muzzleLight.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleLight.SetActive(false);
    }

    /// <summary>
    /// 根据目标高度预判射击偏移
    /// </summary>
    /// <returns></returns>
    private float GetAnticipation(float _height) {
        float height = Mathf.Abs(transform.position.y - _height);

        float speed = target.GetComponent<BBird>().GetCurSpeed();
        float targetDis = speed * time_Shoot;

        return targetDis * anticipationPrecision;
    }

    private void OnDrawGizmos()
    {
        if (!findTarget && !lockTarget)
            Gizmos.color = Color.green;
        else if (findTarget && !lockTarget)
            Gizmos.color = Color.yellow;
        else if (lockTarget)
            Gizmos.color = Color.red;

        Vector2 origin = transform.position + new Vector3(offset.x, offset.y, 0);

        Gizmos.DrawWireSphere(origin, radius);

        if (lockTarget) {
            Gizmos.DrawLine(origin,origin + viewDir);
        }
        
    }
}
