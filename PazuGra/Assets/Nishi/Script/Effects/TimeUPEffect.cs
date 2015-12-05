using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeUPEffect : MonoBehaviour {
    [SerializeField]
    Image m_image1;
    [SerializeField]
    Image m_image2;
    [SerializeField]
    GameObject m_MainEndEffect;

    public void Start()
    {

        LeanTween.moveLocalX(m_image1.gameObject, 0, 2);
        LeanTween.moveLocalX(m_image2.gameObject, 0, 2).setOnComplete(() => { EndStop(); }); ;

    }

    //余韻を残す
    private void EndStop()
    {
        LeanTween.moveLocalX(m_image1.gameObject, 0, 1).setOnComplete(() => { End(); });
    }

    private void End()
    {
        //GameObject.Find("Pausable").GetComponent<Pausable>().PauseSend(false);
        //PrefabManager.Instance.Next(PrefabName.Ranking);
        EffectManager.Instance.Create(m_MainEndEffect, transform.parent);
        Destroy(gameObject);
    }
}
