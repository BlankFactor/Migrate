using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;
public class Begenning : MonoBehaviour
{
    public List<string> stringGroup;

    [Space]
    public Text text;
    public Animator ani;
    private int stringIndex = 0;
    private bool listenning = true;

    [Header("临时使用,开幕")]
    public Volume bloom;
    public Light2D light;
    public float speed_FadeIn = 0.3f;

    private void Start()
    {
        //DisplayText();
        light.intensity = 0.1f;
    }

    private void Update()
    {
        // 临时使用 不保留原开场演出

        if (Input.GetMouseButtonDown(0) && listenning) {
            GameManager.instance.StartGame();
            PlayerController.instance.TakeOffForcibly();
            CameraManager.instance.RemoveTempCamera();

            listenning = !listenning;
            StartCoroutine(FadeIn());
            //this.enabled = false;
            //gameObject.SetActive(false);
        }

        //

        if (listenning) {
            //HandleInput();
        }
    }

    public IEnumerator FadeIn()
    {
        while(bloom.weight!=0 || light.intensity != 1)
        {
            bloom.weight -= Time.deltaTime * speed_FadeIn;
            light.intensity += Time.deltaTime * speed_FadeIn;

            bloom.weight = Mathf.Clamp(bloom.weight, 0, 1);
            light.intensity = Mathf.Clamp(light.intensity, 0, 1);
            yield return Time.deltaTime;
        }

        this.enabled = false;
        gameObject.SetActive(false);
        yield break;

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
