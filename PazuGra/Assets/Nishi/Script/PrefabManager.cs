using UnityEngine;
using System.Collections;

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
    private GameObject[] m_TitlePrefabs = null;
    [SerializeField]
    private GameObject[] m_GameMainPrefabs = null;
    [SerializeField]
    private GameObject[] m_RankingPrefabs = null;

    [SerializeField]
    private Canvas m_DefCanvas = null;


    private int m_PrefabIndex = 0;
    private PrefabName m_CurrentPrefab;
    private bool m_isEnd = false;

    public void Start()
    {
        m_CurrentPrefab = (int)PrefabName.Title;
    }

    //public void Update()
    //{
    //    if(m_CurrentPrefab == PrefabName.Title)
    //    {
    //        foreach (GameObject obj in m_TitlePrefabs)
    //        {
    //            obj.SetActive(true);
    //        }
    //        foreach (GameObject obj in m_GameMainPrefabs)
    //        {
    //            obj.SetActive(false);
    //        }
    //        foreach (GameObject obj in m_RankingPrefabs)
    //        {
    //            obj.SetActive(false);
    //        }
    //    }
    //    else if(m_CurrentPrefab == PrefabName.GameMain)
    //    {
    //        foreach (GameObject obj in m_TitlePrefabs)
    //        {
    //            obj.SetActive(false);
    //        }
    //        foreach (GameObject obj in m_GameMainPrefabs)
    //        {
    //            obj.SetActive(true);
    //        }
    //        foreach (GameObject obj in m_RankingPrefabs)
    //        {
    //            obj.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        foreach (GameObject obj in m_TitlePrefabs)
    //        {
    //            obj.SetActive(false);
    //        }
    //        foreach (GameObject obj in m_GameMainPrefabs)
    //        {
    //            obj.SetActive(false);
    //        }
    //        foreach (GameObject obj in m_RankingPrefabs)
    //        {
    //            obj.SetActive(true);
    //        }
    //    }
    //}

    public void SetIsEnd(bool result)
    {
        m_isEnd = result;
    }

    //public void Next(PrefabName name)
    //{
    //    //ゲームタイマーのReset
    //    if (m_CurrentPrefab == PrefabName.GameMain)
    //    {
    //        GameObject.Find("GameTimer").GetComponent<GameTimer>().TimeReset();
    //        if (GameObject.Find("RollGauges(Clone)") != null)
    //        {
    //            Destroy(GameObject.Find("RollGauges(Clone)").gameObject);
    //        }
    //    }

    //    for(int i = 0; m_DefCanvas.transform.childCount > i;i++)
    //    {
    //        Destroy(m_DefCanvas.transform.GetChild(i).gameObject);
    //    }
    //    m_CurrentPrefab = name;

    //    if(m_CurrentPrefab == PrefabName.Ranking)m_RankingPrefabs[0].transform.GetChild(0).GetComponent<ResultUIEffect>().StartEffect();
    //}

    public void Next(PrefabName name)
    {
        //ゲームタイマーのReset
        if (m_CurrentPrefab == PrefabName.GameMain)
        {
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
            foreach (GameObject obj in m_TitlePrefabs)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in m_GameMainPrefabs)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in m_RankingPrefabs)
            {
                obj.SetActive(false);
            }
        }
        else if (m_CurrentPrefab == PrefabName.GameMain)
        {
            foreach (GameObject obj in m_TitlePrefabs)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in m_GameMainPrefabs)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in m_RankingPrefabs)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in m_TitlePrefabs)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in m_GameMainPrefabs)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in m_RankingPrefabs)
            {
                obj.SetActive(true);
            }
        }

        if (m_CurrentPrefab == PrefabName.Ranking) m_RankingPrefabs[0].transform.GetChild(0).GetComponent<ResultUIEffect>().StartEffect();
    }

    void ActiveChange()
    {
    }
}
