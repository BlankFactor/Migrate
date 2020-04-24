using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventDescription : MonoBehaviour
{
    public Image image;
    public Text text;

    public void DisplayWindow(Sprite _sprite, string _text) {
        image.sprite = _sprite;
        text.text = _text;

        gameObject.SetActive(true);
    }
}
