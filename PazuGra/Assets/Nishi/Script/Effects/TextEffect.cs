using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextEffect : MonoBehaviour
{
    //だんだんと表示する
    void Start()
    {
        Image imag = GetComponent<Image>();
        imag.fillAmount = 0;
        LeanTween.value(gameObject, 0f, 1f, 0.5f)
            .setOnUpdate((float value) => { imag.fillAmount = value; });
    }
}
