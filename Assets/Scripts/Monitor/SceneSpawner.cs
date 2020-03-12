using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawner : MonoBehaviour
{
    public List<GameObject> sceneObject;

    [Header("属性设置")]
    public bool enable;
    public bool setActitve = true;

    [Header("目标Tag")]
    public string targetTag;

    private void Start()
    {
        if (setActitve)
            foreach (var i in sceneObject)
                i.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enable) {
            enable = true;
            SpawnScene(setActitve);
            Destroy(gameObject, 5f);
        }
    }

    protected virtual void SpawnScene(bool _b) {
        if (setActitve)
            foreach (var i in sceneObject)
                i.SetActive(_b);
        else
            foreach (var i in sceneObject)
                Destroy(i);
    }
}
