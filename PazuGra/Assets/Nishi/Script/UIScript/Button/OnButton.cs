using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnButton : MonoBehaviour {

    [SerializeField]
    GameObject m_UI = null;

    public void OnStart()
    {
        GameObject.Find("Pausable").GetComponent<Pausable>().PauseSend(true);
        EffectManager.Instance.Create(m_UI,GameObject.Find("PuauseUI").transform);
        //PrefabManager.Instance.Next(PrefabName.GameMain);
    }

    public void OnRanking()
    {
        PrefabManager.Instance.Next(PrefabName.Ranking);
    }

    public void OnTitle()
    {
        PrefabManager.Instance.Next(PrefabName.Title);
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
        GameObject temp =Instantiate(m_UI);
        temp.transform.SetParent(GameObject.Find("UIs").transform, false);
    }

    public void OnPauseON()
    {
        GameObject temp = Instantiate(m_UI);
        temp.transform.SetParent(GameObject.Find("PuauseUI").transform,false);
        ExecuteEvents.Execute<IPauseSend>(
                GameObject.Find("Pausable"),
                null,
                (events, y) => { events.PauseSend(true); });
    }

    public void OnCancel()
    {
        Destroy(GameObject.Find("UIs").transform.GetChild(0).gameObject);
        ExecuteEvents.Execute<IPauseSend>(
                GameObject.Find("Pausable"),
                null,
                (events, y) => { events.PauseSend(false); });
    }
}
