using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Begenning : MonoBehaviour
{
    public List<string> stringGroup;

    [Space]
    public Text text;
    public Animator ani;
    private int stringIndex = 0;
    private bool listenning = false;

    private void Start()
    {
        DisplayText();
    }

    private void Update()
    {
        if (listenning) {
            //HandleInput();
        }
    }

    public void HandleInput() {
        if (Input.GetMouseButtonDown(0))
            PlayAnimation();
    }

    public void StopAniamtion() {
        ani.speed = 0;
        listenning = true;
    }
    public void PlayAnimation() {
        ani.speed = 1;
        listenning = false;
    }

    public void DisplayText() {
        if (stringIndex == stringGroup.Count) {
            ani.Play("Beginning_FadeOut");
            stringIndex = 0;
            return;
        }

        text.text = stringGroup[stringIndex];
        ani.Play("Beginning_DisplayText");

        stringIndex++;
    }

    public void PrepareToStartGame() {
        GUIController.instance.StartGame();
    }

}
