using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] m_UIs;
    [SerializeField]
    GameObject[] m_Images;

    [SerializeField]
    GameObject m_Dummys;

    int m_NowStep = 0;
    int m_PreviousStep = 0;
    int m_ImageNum = 0;

    GameObject m_CurrentObj1 = null;
    GameObject m_CurrentObj2 = null;

    private static TutorialManager sInstance;

    public static TutorialManager Instance
    {
        get
        {
            if (sInstance == null)
                sInstance = GameObject.FindObjectOfType<TutorialManager>();
            return sInstance;
        }
    }

    public void Update()
    {
        if (m_PreviousStep != m_NowStep)
        {
            Destroy(m_CurrentObj1);
            Destroy(m_CurrentObj2);
            if (m_NowStep == 1 || m_NowStep == 4 || m_NowStep == 5 || m_NowStep == 6)
            {
                m_CurrentObj2 = Instantiate(m_Images[m_ImageNum]);
                m_CurrentObj2.transform.SetParent(transform, false);
                m_ImageNum++;
            }
            m_CurrentObj1 = Instantiate(m_UIs[m_NowStep]);
            m_CurrentObj1.transform.SetParent(transform, false);
            m_PreviousStep = m_NowStep;
        }
        transform.SetAsLastSibling();
    }

    public void StartTutorial()
    {
        GameObject temp = Instantiate(m_Dummys);
        temp.transform.SetParent(transform, false);
        m_CurrentObj1 = Instantiate(m_UIs[0]);
        m_CurrentObj1.transform.SetParent(transform, false);
    }

    public void StepUp()
    {
        m_NowStep++;
        if (m_NowStep == 12)
        {
            AudioManager.Instance.SEPlay(AudioList.TutorialClear);
        }

    }

    public void StepReset()
    {
        GetComponent<Advice>().init();
        Destroy(m_CurrentObj2);
        Destroy(m_CurrentObj1);
        m_ImageNum = 0;
        m_NowStep = 0;
        m_PreviousStep = 0;
    }

    public int GetStep()
    {
        return m_NowStep;
    }
}
