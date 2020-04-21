using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Display()
    {
        animator.Play("Panel_Ending_FadeIn");
    }

    public void Disable() {
        animator.Play("Panel_Ending_FadeOut");
    }
}
