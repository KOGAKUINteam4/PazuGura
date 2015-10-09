using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{

    [SerializeField, Tooltip("タイムの初期値")]
    private float m_timer = 60.0f;

    public Sprite[] m_nums;

    public Image m_1Digit,m_10Digit;

    public void Awake()
    {
        m_10Digit.sprite = m_nums[((int)m_timer / 1) % 10];
        m_1Digit.sprite = m_nums[((int)m_timer / 10) % 10];
    }



    // Update is called once per frame
    void Update()
    {
        m_timer = Clamp(m_timer, 0, 100);

        m_timer -= Time.deltaTime;
        if (!isTimerOver())
        {
            m_10Digit.sprite = m_nums[((int)m_timer / 1) % 10];
            m_1Digit.sprite = m_nums[((int)m_timer / 10) % 10];
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

}
