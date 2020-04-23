using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer_Ending : MonoBehaviour
{
    private List<string> str = new List<string>();
    private Text text;
    private Animator animator;

    private void Awake()
    {
        text = GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void SetStrAndDisplay(List<string> _str) {
        str = _str;
        DisplayText();
    }

    public void DisplayText() {
        if (str.Count.Equals(0)) return;

        text.text = str[0];
        str.RemoveAt(0);
        animator.SetBool("Display", true);
        
    }

    public void ResetPar()
    {
        animator.SetBool("Display", false);
    }
}
