using UnityEngine;
using System.Collections;

public class PageUpdate : MonoBehaviour {

    public void Init()
    {
        GameManager.GetInstanc.GetRanking().Init();
    }

    public void Remove(GameObject target)
    {
        Time.timeScale = 1;
        Destroy(target);
    }
}
