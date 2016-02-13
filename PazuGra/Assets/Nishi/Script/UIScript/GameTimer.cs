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
    private Sprite[] m_Rnums;

    [SerializeField]
    private Image m_1Digit = null,m_10Digit = null, m_100Digit = null;

    [SerializeField]
    private GameObject m_ImagePrefab;
    [SerializeField]
    private GameObject m_EndePrefab;
    [SerializeField]
    private GameObject m_AlertPrefab;

    private bool m_isEffectEnd = false;
    private GameObject m_AlertCurrent;

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

#if DEBUG
        if(Input.GetKey(KeyCode.Space))
        {
            m_Timer--;
        }
#endif
        m_Timer -= Time.deltaTime;
        float display = m_Timer;
        display = Mathf.Clamp(display, 0, 999);
        if (GameManager.GetInstanc.GetTutorial()) m_Timer = 181;
        if (!isTimerOver())
        {
            if (m_Timer < 30)
            {
                m_1Digit.sprite = m_Rnums[((int)display / 1) % 10];
                m_10Digit.sprite = m_Rnums[((int)display / 10) % 10];
                m_100Digit.sprite = m_Rnums[((int)display / 100) % 10];

                Color alpha = new Color(1, 1, 1, Mathf.Lerp(0.3f, 1,1 - Mathf.Sin(270 * m_Timer * Mathf.Deg2Rad)));
                m_1Digit.color = alpha;
                m_10Digit.color = alpha;
                m_100Digit.color = alpha;
                AudioManager.Instance.BGMPlay(AudioList.TimeSlightly);
                if (m_AlertCurrent == null)
                {
                    m_AlertCurrent = Instantiate(m_AlertPrefab);
                    m_AlertCurrent.transform.SetParent(transform,false);
                }
            }
            else
            {
                Destroy(m_AlertCurrent);
                m_1Digit.sprite = m_nums[((int)display / 1) % 10];
                m_10Digit.sprite = m_nums[((int)display / 10) % 10];
                m_100Digit.sprite = m_nums[((int)display / 100) % 10];
            }
        }
        else
        {
            if (!m_isEffectEnd)
            {
                Destroy(m_AlertCurrent);
                AudioManager.Instance.BGMStop();
                GameObject.Find("Pausable").GetComponent<Pausable>().PauseSend(true);
                EffectManager.Instance.Create(m_EndePrefab, GameObject.Find("PuauseUI").transform);
                m_isEffectEnd = true;
            }
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
        m_isEffectEnd = false;

        float display = m_Timer;
        display = Mathf.Clamp(display, 0, 999);
        m_1Digit.sprite = m_nums[((int)display / 1) % 10];
        m_10Digit.sprite = m_nums[((int)display / 10) % 10];
        m_100Digit.sprite = m_nums[((int)display / 100) % 10];

        Color alpha = new Color(1, 1, 1, 1);
        m_1Digit.color = alpha;
        m_10Digit.color = alpha;
        m_100Digit.color = alpha;
    }

    public void ComboSend()
    {
        GameObject temp = Instantiate(m_ImagePrefab);
        temp.transform.SetParent(gameObject.transform,false);
        Destroy(temp,1.2f);
        AddSecond(5);
    }

}
