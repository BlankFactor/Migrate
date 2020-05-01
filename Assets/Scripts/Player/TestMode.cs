using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class TestMode : MonoBehaviour
{
    bool testMode;
    public LeaderBird bird;

    public GameObject Panel;
    public Text vel;
    public Text pos;
    public Text sal;
    public Text ene;
    public Text core;
    public Text voNight;
    public Text vo;
    public Text deadable;
    
    [Space]
    public Volume volume_Night;
    public Volume volume;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!testMode) {
                testMode = true;
                Panel.SetActive(true);
                PlayerController.instance.SetCursor(true);
            }
            else{
                testMode = false;
                Panel.SetActive(false);
                PlayerController.instance.SetCursor(false);
            }


        }


        if (Input.GetKeyDown(KeyCode.Space)) {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        if(testMode){
            if (Input.GetKeyDown(KeyCode.F2))
                bird.Die();

            if (Input.GetKeyDown(KeyCode.F3))
                bird.SetDeadable(!bird.deadable);

            float x = Input.GetAxis("Horizontal");
            Vector3 v3 = bird.transform.position;
            v3.x += x * Time.deltaTime * 300f;
            bird.transform.position = v3;

            vel.text = bird.cur_Speed.ToString("0.0");
            pos.text = bird.transform.position.x.ToString("0.0");
            sal.text = bird.cur_Satiety.ToString("0.0");
            ene.text = bird.cur_Energy.ToString("0.0");
            core.text = bird.cur_CoreEnergy.ToString("0.0");
            vo.text = volume.weight.ToString("0.00");
            voNight.text = volume_Night.weight.ToString("0.00");
            deadable.text = bird.deadable.ToString();
        }
}
}
