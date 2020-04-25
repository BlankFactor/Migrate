using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer_Ending : MonoBehaviour
{
    private List<string> str = new List<string>();
    private Text text;
    private Animator animator;
    int count = 0;
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
        if (str.Count.Equals(count)) return;
        text.text = str[count];
        count++;
        animator.SetBool("Display", true);
        
    }

    public void ResetPar()
    {
        animator.SetBool("Display", false);

        if (!str.Count.Equals(count))
        {
            text.text = str[count];
            count++;
            animator.SetBool("Display", true);
        }
        else {
            GUIController.instance.Display_Panel_Ending();
        }
    }
}
