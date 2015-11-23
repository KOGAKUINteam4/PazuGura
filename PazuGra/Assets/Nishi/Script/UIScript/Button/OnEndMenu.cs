using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnEndMenu : MonoBehaviour {

    public void OnGameEndYes()
    {
        Destroy(gameObject.transform.parent.parent.gameObject);
        PrefabManager.Instance.Next(PrefabName.Title); ;
        ExecuteEvents.Execute<IPauseSend>(
                GameObject.Find("Pausable"),
                null,
                (events, y) => { events.PauseSend(false); });
    }

    public void OnRestartYes()
    {
        Destroy(gameObject.transform.parent.parent.gameObject);
        PrefabManager.Instance.Next(PrefabName.GameMain);
        ExecuteEvents.Execute<IPauseSend>(
                GameObject.Find("Pausable"),
                null,
                (events, y) => { events.PauseSend(false); });
    }

    public void OnNo()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
