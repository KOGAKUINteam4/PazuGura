using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextEffect : MonoBehaviour
{

    public float m_Timer = 0.5f;
    //だんだんと表示する
    void Start()
    {
        Image imag = GetComponent<Image>();
        imag.fillAmount = 0;
        LeanTween.value(gameObject, 0f, 1f, m_Timer)
            .setOnUpdate((float value) => { imag.fillAmount = value; });
    }
}
