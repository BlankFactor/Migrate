using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmVolumeSetter : MonoBehaviour
{
    public bool enable;
    public bool fadeIn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.tag.Equals("LeaderBird")) return; 

        if (!enable)
            enable = true;

            if (fadeIn)
            {
                GlobalAudioPlayer.instance.SwitchBgm();
                GlobalAudioPlayer.instance.StartBgmFadeIn();
            }
            else
            {
                GlobalAudioPlayer.instance.StartBgmFadeOut();
            }

        Destroy(gameObject, 5f);  
    }
}
