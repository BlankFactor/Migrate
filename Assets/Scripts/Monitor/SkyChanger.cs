using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyChanger : MonoBehaviour
{
    public bool enable;

    public SpriteRenderer render;
    public Sprite sky_Backup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "LeaderBird") return;

        if (!enable)
        {
            enable = true;

            render.sprite = sky_Backup;            
        }
    }
}
