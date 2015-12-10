using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleCallEffect : MonoBehaviour {

    [SerializeField]
    Image m_Shadow = null;

    [SerializeField]
    PrefabName m_name = PrefabName.Ranking;

	// Use this for initialization
	void Start () {
        LeanTween.alpha(m_Shadow.rectTransform, 1, 1).setOnComplete(() => { Open(); });
	}

    void Open()
    {
        LeanTween.alpha(m_Shadow.rectTransform, 0, 0.5f).setOnComplete(() => { End(); });
        PrefabManager.Instance.Next(m_name);
    }

    void End()
    {
        Destroy(gameObject);
    }

    public void NextChange(PrefabName name)
    {
        m_name = name;
    }
}
