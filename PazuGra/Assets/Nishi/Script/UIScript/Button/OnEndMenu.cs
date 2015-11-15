using UnityEngine;
using System.Collections;

public class OnEndMenu : MonoBehaviour {

    public void OnGameEndYes()
    {
        Destroy(gameObject.transform.parent.parent);
        PrefabManager.Instance.Next(PrefabName.Title);
    }

    public void OnRestartYes()
    {
        Destroy(gameObject.transform.parent.parent);
        PrefabManager.Instance.Next(PrefabName.GameMain);
    }

    public void OnNo()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
