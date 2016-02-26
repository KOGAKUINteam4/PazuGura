using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankingEffect : MonoBehaviour
{

    public bool isCheck = false;

    private Vector3 m_to = new Vector3(1.1f, 1.1f, 1f);
    private Vector3 m_From = new Vector3(1f, 1f, 1f);
    private float m_Timer = 0;

    public void Update()
    {
        if (!isCheck)
        {
            RectTransform[] rect = gameObject.GetComponentsInChildren<RectTransform>();
            foreach (var r in rect)
            {
                r.localScale = Vector3.one;
            }
            m_to = new Vector3(1.2f, 1.2f, 1f);
            m_From = new Vector3(1f, 1f, 1f);
            Image[] ima = gameObject.GetComponentsInChildren<Image>();
            foreach (var img in ima)
            {
                img.color = new Color(1f, 1f, 1f, 1f);
            }
            return;
        }

        Image[] images = gameObject.GetComponentsInChildren<Image>();
        foreach (var img in images)
        {
            img.color = new Color(1f, 0.5f, 0.5f, 1f);
        }
        Scale();

    }

    void Scale()
    {
        m_Timer += Time.deltaTime;
        Vector3 S = Vector3.Lerp(m_From, m_to, m_Timer);
        if(m_Timer >= 1)
        {
            Vector3 temp = m_to;
            m_to = m_From;
            m_From = temp;
            m_Timer = 0;
        }
        RectTransform[] rect = gameObject.GetComponentsInChildren<RectTransform>();
        foreach (var r in rect)
        {
            r.localScale = S;
        }
    }
}