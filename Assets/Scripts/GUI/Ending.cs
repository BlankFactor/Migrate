using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    private Animator animator;
    public Text text;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    public void Display()
    {
        animator.Play("Panel_Ending_FadeIn");
    }

    public void DisplayText() {
        text.gameObject.SetActive(true);
    }

    public void Disable() {
        animator.Play("Panel_Ending_FadeOut");
    }
}
