using UnityEngine;
using System.Collections;

public class OnButton : MonoBehaviour {

    public void OnStart()
    {
        PrefabManager.Instance.Next(PrefabName.GameMain);
    }
}
