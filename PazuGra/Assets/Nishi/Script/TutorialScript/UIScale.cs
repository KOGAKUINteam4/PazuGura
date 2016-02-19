using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScale : MonoBehaviour {

    Image m_image = null;

	// Use this for initialization
	void Start ()
    {
        m_image = GetComponent<Image>();
        ScaleDown();
	
	}

    void ScaleUp()
    {
        LeanTween.scale(m_image.rectTransform, new Vector3(0.8f, 0.8f, 1f), 1f).setOnComplete(() => { ScaleDown(); });
    }

    void ScaleDown()
    {
        LeanTween.scale(m_image.rectTransform, new Vector3(1f, 1f, 1f), 1f).setOnComplete(() => { ScaleUp(); });
    }
}
