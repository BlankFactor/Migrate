using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poaching : BEvent
{
    GameObject poacher;

    public Poaching()
    {
        illu = Resources.Load<Sprite>("test");
        desc = "猎人来了";

        poacher = Resources.Load<GameObject>("Prefabs\\Poacher_Res");
    }

    public override void Execute(LeaderBird _lb)
    {
        int count = _lb.birds.Count;

        if (count >= 1)
        {
            int index = Random.Range(0, count);

            _lb.birds[index].Die();
            GUIController.instance.AddString("你鸟死了");

            EventRecorder.instance.Add_Poarching(1);
        }
        else {
            GUIController.instance.AddString("很幸运 你溜了");
        }

        GUIController.instance.Shutdown_Clock();

        GlobalAudioPlayer.instance.Play_Gunfire();

        Collider2D[] cols = Physics2D.OverlapCircleAll(_lb.transform.position, 30.0f, LayerMask.GetMask("PoacherSpawnPoint"));
        if (cols.Length >= 1)
        {
            GameObject poa = GameObject.Instantiate(poacher, cols[0].transform.position, Quaternion.identity);
        }

        GameObject.Find("PlayerController").GetComponent<PlayerController>().TakeOffForcibly();
    }

    public override string GetDesc()
    {
        return desc;
    }

    public override Sprite GetIllu()
    {
        return illu;
    }

    public override void Undo(LeaderBird _lb)
    {

    }
}
