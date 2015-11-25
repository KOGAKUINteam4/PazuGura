using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComboCounter : MonoBehaviour, IRecieveMessage {

    [SerializeField]
    private Sprite[] m_nums;
    [SerializeField]
    private Image m_1Digit = null, m_10Digit = null;
    [SerializeField]
    private GameObject m_GameTimer;

    private int m_OrdCount = 0;
    private Vector3  fixPosition;
    //コンボカウンター
    private int m_counter = 1;

    // Use this for initialization
    void Start () {
        m_OrdCount = 0;
        m_GameTimer = GameObject.Find("GameTimer");
        m_1Digit.enabled = true;
        m_10Digit.enabled = false;
        //　　　　　　　　　　　　　　　　　　　　　　　　　　　A案はXが0.2f
        fixPosition = m_1Digit.rectTransform.position + new Vector3(0.3f,0f,0f);
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_counter >= 10)
        {
            m_10Digit.enabled = true;
            m_1Digit.rectTransform.position = fixPosition;
        }

        m_10Digit.sprite = m_nums[(m_counter /10) % 10];
        m_1Digit.sprite = m_nums[(m_counter / 1) % 10];

        if(CheckMultiple())
        {
            ExecuteEvents.Execute<IRecieveMessage>(
                m_GameTimer,
                null,
                (events, y) => { events.ComboSend(); });
        }

    }

    private bool CheckMultiple(int i = 3)
    {
        
        if (m_counter % i == 0)
        {
            if (m_OrdCount != m_counter)
            {
                m_OrdCount = m_counter;
                return true;
            }
        }
        return false;
    }

    public void ComboSend()
    {
        m_counter++;
    }

    public int GetCombo()
    {
        return m_counter;
    }

}
