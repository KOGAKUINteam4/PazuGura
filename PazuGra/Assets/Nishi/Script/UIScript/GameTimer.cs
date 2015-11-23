using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameTimer : MonoBehaviour, IRecieveMessage
{

    private float m_Timer;

    [SerializeField, Tooltip("タイムの初期値")]
    private float m_RestTimer = 180.0f;

    [SerializeField]
    private Sprite[] m_nums;

    [SerializeField]
    private Image m_1Digit = null,m_10Digit = null, m_100Digit = null;

    [SerializeField]
    private GameObject m_ImagePrefab;

    public void Awake()
    {
        m_Timer = m_RestTimer;
        m_1Digit.sprite = m_nums[((int)m_Timer / 1) % 10];
        m_10Digit.sprite = m_nums[((int)m_Timer / 10) % 10];
        m_100Digit.sprite = m_nums[((int)m_Timer / 100) % 10];
    }



    // Update is called once per frame
    void Update()
    {
        m_Timer -= Time.deltaTime;
        float display = m_Timer;
        display = Mathf.Clamp(display, 0, 999);
        if (!isTimerOver())
        {
            m_1Digit.sprite = m_nums[((int)display / 1) % 10];
            m_10Digit.sprite = m_nums[((int)display / 10) % 10];
            m_100Digit.sprite = m_nums[((int)display / 100) % 10];
        }
        else
        {
            PrefabManager.Instance.Next(PrefabName.Ranking);
            Debug.Log("End");
        }
    }

    public bool isTimerOver()
    {
        return m_Timer <= 0;
    }

    public void AddSecond(float add)
    {
        m_Timer += add;
    }

    public void TimeReset()
    {
        m_Timer = m_RestTimer;
    }

    public void ComboSend()
    {
        GameObject temp = Instantiate(m_ImagePrefab);
        temp.transform.SetParent(gameObject.transform,false);
        Destroy(temp,1.2f);
        AddSecond(3);
    }

}
