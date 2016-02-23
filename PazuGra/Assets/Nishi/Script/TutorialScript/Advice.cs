using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Advice : MonoBehaviour {

    GameObject m_Currentobj = null;
    [SerializeField]
    GameObject[] m_Advice = null;

    bool[] m_isDisplay = new bool[3];

	// Use this for initialization
	void Start ()
    {
        init();
	}

	void Update ()
    {
        if (TutorialManager.Instance.GetStep() >= 11)
        {
            if(GameObject.Find("GameTimer").GetComponent<GameTimer>().GetTime() < 45 && !m_isDisplay[0])
            {
                m_Currentobj = Instantiate(m_Advice[2]);
                m_Currentobj.transform.SetParent(transform, false);
                Destroy(m_Currentobj, 5f);
                m_isDisplay[0] = true;
            }
            else if(GameObject.Find("GameTimer").GetComponent<GameTimer>().GetTime() < 65 && !m_isDisplay[1])
            {
                m_Currentobj = Instantiate(m_Advice[1]);
                m_Currentobj.transform.SetParent(transform, false);
                Destroy(m_Currentobj, 5f);
                m_isDisplay[1] = true;
            }
            else if (GameObject.Find("GameTimer").GetComponent<GameTimer>().GetTime() < 85 && !m_isDisplay[2])
            {
                m_Currentobj = Instantiate(m_Advice[0]);
                m_Currentobj.transform.SetParent(transform, false);
                Destroy(m_Currentobj, 5f);
                m_isDisplay[2] = true;
            }
        }
	}

    public void init()
    {
        m_isDisplay[0] = false;
        m_isDisplay[1] = false;
        m_isDisplay[2] = false;
        Destroy(m_Currentobj);
    }
}
