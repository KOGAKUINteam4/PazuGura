using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour {
    private static EffectManager sInstance;

    public static EffectManager Instance
    {
        get
        {
            if (sInstance == null)
                sInstance = GameObject.FindObjectOfType<EffectManager>();
            return sInstance;
        }
    }

    public void Create(GameObject prefab)
    {
        Instantiate(prefab);
    }
    public void Create(GameObject prefab,Transform parent)
    {
        GameObject Temp = Instantiate(prefab);
        Temp.transform.SetParent(parent, false);
    }

    public void Create(Vector3 position, GameObject prefab)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
