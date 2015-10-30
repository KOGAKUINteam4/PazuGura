using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameTimer : MonoBehaviour, IRecieveMessage
{

    [SerializeField, Tooltip("タイムの初期値")]
    private float m_timer = 10.0f;

    [SerializeField]
    private Sprite[] m_nums;

    [SerializeField]
    private Image m_1Digit,m_10Digit, m_100Digit;

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
        display = Clamp(display, 0, 999);
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

        //デバック用
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddSecond(1);
        }
        //-----------------------------------
    }

    public bool isTimerOver()
    {
        return m_timer <= 0;
    }

    public void AddSecond(float add)
    {
        m_timer += add;
    }

    float Clamp(float value, float min, float max)
    {
        if(value > max)
        {
            value = max;
        }
        else if(value < min)
        {
            value = min;
        }

        return value;
    }

    public void ComboStart()
    {
        Destroy();
        AddSecond(3);
    }

}
