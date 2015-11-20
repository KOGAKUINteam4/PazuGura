using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameTimer : MonoBehaviour, IRecieveMessage
{

    [SerializeField, Tooltip("タイムの初期値")]
    private float m_timer = 180.0f;

    [SerializeField]
    private Sprite[] m_nums;

    [SerializeField]
    private Image m_1Digit = null,m_10Digit = null, m_100Digit = null;

    [SerializeField]
    private GameObject m_ImagePrefab;

    public void Awake()
    {
        m_1Digit.sprite = m_nums[((int)m_timer / 1) % 10];
        m_10Digit.sprite = m_nums[((int)m_timer / 10) % 10];
        m_100Digit.sprite = m_nums[((int)m_timer / 100) % 10];
    }



    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;
        float display = m_timer;
        display = Mathf.Clamp(display, 0, 999);
        if (!isTimerOver())
        {
            m_1Digit.sprite = m_nums[((int)display / 1) % 10];
            m_10Digit.sprite = m_nums[((int)display / 10) % 10];
            m_100Digit.sprite = m_nums[((int)display / 100) % 10];
        }
        else
        {
            Debug.Log("End");
        }
    }

    public bool isTimerOver()
    {
        return m_timer <= 0;
    }

    public void AddSecond(float add)
    {
        m_timer += add;
    }

    public void TimeReset()
    {
        m_timer = 180;
    }

    public void ComboSend()
    {
        GameObject temp = Instantiate(m_ImagePrefab);
        temp.transform.SetParent(gameObject.transform,false);
        Destroy(temp,1.2f);
        AddSecond(3);
    }

}
