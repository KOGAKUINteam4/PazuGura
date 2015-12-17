using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnButton : MonoBehaviour
{

    [SerializeField]
    GameObject m_UI = null;

    

    public void Start()
    {
        if (gameObject.GetComponent<Animator>() != null)
        {
            Animator anim = gameObject.GetComponent<Animator>();
            anim.Rebind();
        }
    }

    public void OnStart()
    {
        AudioManager.Instance.SEPlay(AudioList.Yes);
        GameObject.Find("Pausable").GetComponent<Pausable>().PauseSend(true);
        EffectManager.Instance.Create(m_UI, GameObject.Find("PuauseUI").transform);
        //PrefabManager.Instance.Next(PrefabName.GameMain);
    }

    public void OnRanking()
    {
        AudioManager.Instance.SEPlay(AudioList.Yes);
        //PrefabManager.Instance.Next(PrefabName.Ranking);
        m_UI.GetComponent<TitleCallEffect>().NextChange(PrefabName.Ranking);
        EffectManager.Instance.Create(m_UI, GameObject.Find("PuauseUI").transform);
    }

    public void OnTitle()
    {
        m_UI.GetComponent<TitleCallEffect>().NextChange(PrefabName.Title);
        EffectManager.Instance.Create(m_UI, GameObject.Find("PuauseUI").transform);
    }

    public void OnGameMain()
    {
        PrefabManager.Instance.Next(PrefabName.GameMain);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnGameEND()
    {
        //Destroy(GameObject.Find("UIs").transform.GetChild(0).gameObject);
        //PrefabManager.Instance.Next(PrefabName.Title);
        GameObject temp = Instantiate(m_UI);
        temp.transform.SetParent(GameObject.Find("UIs").transform, false);
    }

    public void OnReStart()
    {
        //Destroy(GameObject.Find("UIs").transform.GetChild(0).gameObject);
        GameObject temp = Instantiate(m_UI);
        temp.transform.SetParent(GameObject.Find("UIs").transform, false);
    }

    public void OnPauseON()
    {
        AudioManager.Instance.SEPlay(AudioList.Pause);
        GameObject temp = Instantiate(m_UI);
        temp.transform.SetParent(GameObject.Find("PuauseUI").transform, false);
        ExecuteEvents.Execute<IPauseSend>(
                GameObject.Find("Pausable"),
                null,
                (events, y) => { events.PauseSend(true); });
    }

    public void OnCancel()
    {
        AudioManager.Instance.SEPlay(AudioList.Cancel);
        Destroy(GameObject.Find("UIs").transform.GetChild(0).gameObject);
        ExecuteEvents.Execute<IPauseSend>(
                GameObject.Find("Pausable"),
                null,
                (events, y) => { events.PauseSend(false); });
    }

    public void OnInitialized()
    {
        GameManager.GetInstanc.GetComponent<GyroGravity>().Reset();
    }
}
