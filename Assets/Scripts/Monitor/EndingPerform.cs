using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPerform : MonoBehaviour
{
    public bool trig;
    public LeaderBird lb;

    public GameObject RenderCam;

    private void Update()
    {
        if (!trig) return;

        Vector3 v3 = RenderCam.transform.position;
        v3.x = lb.transform.position.x;
        RenderCam.transform.position = v3;
    }

    IEnumerator ChangeHeight() {
        while (!lb.transform.position.y.Equals(19.2f)) {
            lb.ChangeHeight(19.2f);
            yield return Time.deltaTime;
        }
    }

    IEnumerator DisplayEndingPanel() {
        yield return new WaitForSeconds(40);

        GUIController.instance.Display_Panel_Ending();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trig) return;
        if (collision.transform.tag.Equals("LeaderBird"))
        {
            trig = true;
            WorldTimeManager.instance.SetStop(true);
            WorldTimeManager.instance.SetKeepDayNight(true);
            PlayerController.instance.SetControlable(false);
            CameraManager.instance.ConverToTelephotoCamera();
            GUIController.instance.Display_Text_Ending();
            GUIController.instance.Disable_EnergyBar();
            lb.SetTired(false);
            lb.isSpeedingUp = false;
            StartCoroutine(ChangeHeight());
        }
    }
}
