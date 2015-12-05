using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PrefabName
{
    Title = 0,
    GameMain,
    Ranking
}

public class PrefabManager : MonoBehaviour
{
    private static PrefabManager sInstance;

    public static PrefabManager Instance
    {
        get
        {
            if (sInstance == null)
                sInstance = GameObject.FindObjectOfType<PrefabManager>();
            return sInstance;
        }
    }

    [SerializeField]
    private GameObject[] m_TitleUis = null;
    [SerializeField]
    private GameObject[] m_GameMainUis = null;
    [SerializeField]
    private GameObject[] m_RankingUis = null;

    [SerializeField]
    private GameObject[] m_TitleEnables = null;
    [SerializeField]
    private GameObject[] m_GameMainEnables = null;
    [SerializeField]
    private GameObject[] m_RankingEnables = null;

    [SerializeField]
    private Canvas m_DefCanvas = null;


    private GameObject[] m_CurrentEnables = null;


    //private int m_PrefabIndex = 0;
    private PrefabName m_CurrentPrefab;
    //private PrefabName m_prevPrefab;
    //private bool m_isEnd = false;

    public void Start()
    {
        m_CurrentPrefab = (int)PrefabName.Title;
        //m_prevPrefab = m_CurrentPrefab;
        m_CurrentEnables = m_TitleEnables;
        ActiveTitle();
    }

    public void Update()
    {
        {
            if (GameObject.Find("Pausable").GetComponent<Pausable>().IsPause())
            {
                scriptEnables(false);
            }
            else
            {
                scriptEnables(true);
            }

        }
    }

    public void Next(PrefabName name)
    {
        //ゲームタイマーのReset
        if (m_CurrentPrefab == PrefabName.GameMain)
        {
            GameObject.Find("GameTimer").GetComponent<GameTimer>().enabled = true;
            GameObject.Find("GameTimer").GetComponent<GameTimer>().TimeReset();
            if (GameObject.Find("RollGauges(Clone)") != null)
            {
                Destroy(GameObject.Find("RollGauges(Clone)").gameObject);
            }
        }

        for (int i = 0; m_DefCanvas.transform.childCount > i; i++)
        {
            Destroy(m_DefCanvas.transform.GetChild(i).gameObject);
        }
        m_CurrentPrefab = name;

        if (m_CurrentPrefab == PrefabName.Title)
        {
            scriptEnables(false);
            m_CurrentEnables = m_TitleEnables;
            ActiveTitle();
            GameObject.Find("GamaManager").GetComponent<ChainManager>().InitChain();
        }
        else if (m_CurrentPrefab == PrefabName.GameMain)
        {
            scriptEnables(false);
            m_CurrentEnables = m_GameMainEnables;
            ActiveMain();
            GameObject.Find("Stamp_00").GetComponent<StampCounter>().InitStamp();
            GameObject.Find("Factory").GetComponent<BlockFactory>().isRainbow = false;
            GameObject.Find("ScoreParent").GetComponent<ScoreUI>().Init();
            GameObject.Find("ScoreParent").GetComponent<ScoreUI>().InitScore();
            GameObject.Find("GamaManager").GetComponent<ChainManager>().InitChain();
            GameObject.Find("ScoreParent").GetComponent<ScoreUI>().ScoreEvent(0);
            GameObject.Find("StampTemplate").GetComponent<TemplateStampPoint.TemplateStamp>().Init();
        }
        else
        {
            scriptEnables(false);
            m_CurrentEnables = m_RankingEnables;
            ActiveRanking();
            m_RankingUis[0].transform.GetChild(0).GetComponent<ResultUIEffect>().StartEffect();
        }

        
    }

    public PrefabName GetCurrentPrefab()
    {
        return m_CurrentPrefab;
    }

    void ActiveTitle()
    {
        foreach (GameObject obj in m_TitleUis)
        {
            //foreach (Image img in obj.GetComponentsInChildren<Image>())
            //{
            //    img.enabled = true;
            //}
            obj.SetActive(true);
        }
        foreach (GameObject obj in m_GameMainUis)
        {
            //foreach (Image img in obj.GetComponentsInChildren<Image>())
            //{
            //    img.enabled = false;
            //}
            obj.SetActive(false);
        }
        foreach (GameObject obj in m_RankingUis)
        {
            //foreach (Image img in obj.GetComponentsInChildren<Image>())
            //{
            //    img.enabled = false;
            //}
            obj.SetActive(false);
        }
    }

    void ActiveMain()
    {
        foreach (GameObject obj in m_TitleUis)
        {
            //foreach (Image img in obj.GetComponentsInChildren<Image>())
            //{
            //    img.enabled = false;
            //}
            obj.SetActive(false);
        }
        foreach (GameObject obj in m_GameMainUis)
        {
            //foreach (Image img in obj.GetComponentsInChildren<Image>())
            //{
            //    img.enabled = true;
            //}
            obj.SetActive(true);
        }
        foreach (GameObject obj in m_RankingUis)
        {
            //foreach (Image img in obj.GetComponentsInChildren<Image>())
            //{
            //    img.enabled = false;
            //}
            obj.SetActive(false);
        }

    }

    void ActiveRanking()
    {

        foreach (GameObject obj in m_TitleUis)
        {
            //foreach (Image img in obj.GetComponentsInChildren<Image>())
            //{
            //    img.enabled = false;
            //}
            obj.SetActive(false);
        }
        foreach (GameObject obj in m_GameMainUis)
        {
            //foreach (Image img in obj.GetComponentsInChildren<Image>())
            //{
            //    img.enabled = false;
            //}
            obj.SetActive(false);
        }
        foreach (GameObject obj in m_RankingUis)
        {
            //foreach (Image img in obj.GetComponentsInChildren<Image>())
            //{
            //    img.enabled = true;
            //}
            obj.SetActive(true);
        }
    }

    void scriptEnables(bool result)
    {
        foreach (GameObject obj in m_CurrentEnables)
        {
            foreach (MonoBehaviour mono in obj.GetComponents<MonoBehaviour>())
            {
                if (!(mono as Image))
                {
                    mono.enabled = result;
                }
            }
        }
    }

}
