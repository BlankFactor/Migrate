using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager instance;

    [Header("其他鸟类生成设定")]
    public Vector2 spawnAreaSize;
    public Vector2 spawnAreaOffset;

    private bool spawnSuccessed = true;

    [Space]
    public float minSpawnTime;
    public float maxSpawnTime;

    [Space]
    [Range(0, 100)]
    public float probability_SpawnMultipleBird = 50f;
    [Range(1,5)]
    public int maxCountPerSpawn = 1;

    [Header("对象及组件")]
    public LeaderBird bird;
    public GameObject keepFlyingBird;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartToCreatBird();
    }

    /// <summary>
    /// 开始创建背景鸟群
    /// </summary>
    private void StartToCreatBird()
    {
        if (bird.isFlying && bird.alive)
        {
            if (spawnSuccessed)
            {
                spawnSuccessed = false;

                StartCoroutine(CreateBird(Random.Range(minSpawnTime, maxSpawnTime)));
            }
        }
    }

    IEnumerator CreateBird(float _time)
    {
        if (bird == null || !bird.alive) yield break;
        yield return new WaitForSeconds(_time);
        if (bird == null || !bird.alive) yield break;

        spawnSuccessed = true;

        Vector3 pos = new Vector3(spawnAreaOffset.x + bird.transform.position.x, spawnAreaOffset.y);
        pos.x += Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        pos.y += Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        
        // 随机化大小和速度
        float maxSpeed = Random.Range(1, 4f);
        float scale = Random.Range(0.3f, 0.8f);

        GameObject go = GameObject.Instantiate(keepFlyingBird, pos, Quaternion.identity);

        go.GetComponent<KeepFlyingBird>().Initial(maxSpeed,scale);

        // 概率生成鸟群
        if (Random.Range(0, 100f) < probability_SpawnMultipleBird) {
            int count = Random.Range(1, maxCountPerSpawn);

            while (count-- != 0) {
                Vector2 npos = go.transform.position;

                npos.x += Random.Range(-5, 5f);
                npos.y += Random.Range(-2, 2f);

                GameObject ngo = Instantiate(keepFlyingBird, npos, Quaternion.identity);
                ngo.GetComponent<KeepFlyingBird>().Initial(maxSpeed, scale);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Spawn Area
        Gizmos.color = new Color(0, 0.5f, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(spawnAreaOffset.x + bird.transform.position.x, spawnAreaOffset.y), new Vector3(spawnAreaSize.x, spawnAreaSize.y));
    }
}
