using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainCallEffect : MonoBehaviour {

    [SerializeField]
    Image m_Hole;
    [SerializeField]
    Image m_Shadow;
    [SerializeField]
    GameObject m_Ready;

    // Use this for initialization
    void Start () {
        LeanTween.alpha(m_Hole.rectTransform, 1, 3);
        LeanTween.alpha(m_Shadow.rectTransform, 1, 3).setOnComplete(()=> { Move(); });

    }

    void Move()
    {
        //gameメインを登場させる
        PrefabManager.Instance.Next(PrefabName.GameMain);
        LeanTween.moveY(gameObject, -9, 3).setOnComplete(() => { Scale(); ScaleShadow(); });
    }

    void Scale()
    {
        LeanTween.scale(m_Hole.rectTransform, new Vector3(7.0f, 7.0f, 0.0f), 5).setOnComplete(()=> { End(); });
    }

    void ScaleShadow()
    {
        LeanTween.scale(m_Shadow.rectTransform, new Vector3(0.5f, 0.5f, 0.0f), 0.5f).setOnComplete(() => {  });
    }

    void End()
    {
        EffectManager.Instance.Create(m_Ready,transform.parent);
        //GameObject.Find("Pausable").GetComponent<Pausable>().PauseSend(false);
        Destroy(gameObject);
    }
}
