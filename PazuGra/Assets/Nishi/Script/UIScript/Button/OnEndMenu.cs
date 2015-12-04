using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnEndMenu : MonoBehaviour {

    [SerializeField]
    private GameObject m_Ready;

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
                (events, y) => { events.PauseSend(true); });
        EffectManager.Instance.Create(m_Ready, GameObject.Find("PuauseUI").transform);
    }

    public void OnNo()
    {
        Back();
        //Destroy(gameObject.transform.parent.gameObject);
    }

    public void Back()
    {
        GameObject temp = GameObject.Find("PausaImage(Clone)");
        LeanTween.moveLocalX(temp.transform.GetChild(1).gameObject, 0, 0.5f).setDelay(0.5f); ;
        LeanTween.moveLocalX(temp.transform.GetChild(2).gameObject, 0, 0.5f).setDelay(0.3f);
        LeanTween.moveLocalX(temp.transform.GetChild(3).gameObject, 0, 0.5f).setDelay(0.1f);
        LeanTween.moveLocalX(temp.transform.GetChild(4).gameObject, 0, 0.5f).setOnComplete(() => { Destroy(gameObject.transform.parent.gameObject); });
    }
}
