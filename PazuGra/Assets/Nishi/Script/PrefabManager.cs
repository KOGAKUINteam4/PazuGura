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
    private GameObject[] m_Prefabs;

    private int m_PrefabIndex = 0;
    private GameObject m_CurrentPrefab;
    private bool m_isEnd = false;

    public void Start()
    {
        m_CurrentPrefab = Instantiate(m_Prefabs[(int)PrefabName.Title]);
    }

    public void Update()
    {
        if(m_isEnd)
        {
            PrefabChange();
            m_isEnd = false;
        }
    }

    private void PrefabChange()
    {
        Destroy(m_CurrentPrefab);
        m_CurrentPrefab = Instantiate(m_Prefabs[m_PrefabIndex]);
    }

    public void SetIsEnd(bool result)
    {
        m_isEnd = result;
    }

    public void Next(PrefabName name)
    {
        m_PrefabIndex = (int)name;
        m_isEnd = true;
    }
}
