﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainEndEffect : MonoBehaviour {

    [SerializeField]
    Image m_Hole;
    [SerializeField]
    Image m_Shadow;

    // Use this for initialization
    void Start ()
    {
        LeanTween.scale(m_Hole.rectTransform, new Vector3(1.0f, 1.0f, 0.0f), 2).setOnComplete(() => { Move(); });
    }

    void Move()
    {
        LeanTween.moveY(gameObject, -12, 2.5f).setOnComplete(() => { Alpha();});
    }

    void Alpha()
    {
        PrefabManager.Instance.Next(PrefabName.Ranking);
        FindObjectOfType<ResultUIEffect>().StartEffect();
        LeanTween.alpha(m_Shadow.rectTransform, 0, 2).setOnComplete(() => { End(); });
    }

    void End()
    {
        GameObject.Find("Pausable").GetComponent<Pausable>().PauseSend(false);
        Destroy(gameObject);
    }
}
