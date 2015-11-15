using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnButton : MonoBehaviour {

    [SerializeField]
    GameObject m_PauseUI = null;

    public void OnStart()
    {
        PrefabManager.Instance.Next(PrefabName.GameMain);
    }

    public void OnGameEND()
    {
        //Destroy(GameObject.Find("UIs").transform.GetChild(0).gameObject);
        //PrefabManager.Instance.Next(PrefabName.Title);
        GameObject temp = Instantiate(m_PauseUI);
        temp.transform.SetParent(GameObject.Find("UIs").transform, false);
    }

    public void OnReStart()
    {
        //Destroy(GameObject.Find("UIs").transform.GetChild(0).gameObject);
        GameObject temp =Instantiate(m_PauseUI);
        temp.transform.SetParent(GameObject.Find("UIs").transform, false);
    }

    public void OnPauseON()
    {
        GameObject temp = Instantiate(m_PauseUI);
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
