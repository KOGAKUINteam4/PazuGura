using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RollGauge : MonoBehaviour , IRecieveMessage {

    [SerializeField]
    float m_maxValue = 10.0f;
    float m_maxtimer;
    [SerializeField]
    Image m_Image;

    // Use this for initialization
    void Start()
    {
        m_Image = m_Image.GetComponent<Image>();
        m_maxtimer = m_maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        m_maxtimer -= Time.deltaTime;
        float value = (m_maxtimer / m_maxValue) * 1;
        if (m_maxtimer > 0.0f)
        {
            m_Image.fillAmount = value;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void ComboSend()
    {
        m_maxtimer = m_maxValue;
    }
}
