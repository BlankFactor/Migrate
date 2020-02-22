using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBorderTrigger : MonoBehaviour
{
    public PolygonCollider2D pc;

    public void CreateTrigger(float _dis,float _offset,float _height) {
        Vector2[] points = pc.GetPath(0);

        points[1].x = -(_dis);
        points[1].y = (_offset + _height);

        points[2].x = -(_dis);
        points[2].y = (_offset - _height);

        pc.SetPath(0, points);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("FollowerBird")) {
            collision.SendMessage("SetInBorder", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("FollowerBird"))
        {
            collision.SendMessage("SetInBorder", false);
        }
    }
}
