using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComboGauge : MonoBehaviour, IRecieveMessage
{

    [SerializeField]
    float m_maxValue = 10.0f;
    float m_maxtimer;
    Slider m_slider;


	// Use this for initialization
	void Start () {
        m_slider = GetComponent<Slider>();
        m_slider.maxValue = m_maxValue;
        m_maxtimer = m_maxValue;

    }
	
	// Update is called once per frame
	void Update () {
        m_maxtimer -= Time.deltaTime;
        if (m_maxtimer > 0.0f)
        {
            m_slider.value = m_maxtimer;
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
