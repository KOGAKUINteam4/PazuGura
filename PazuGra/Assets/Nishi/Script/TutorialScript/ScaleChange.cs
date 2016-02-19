using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject button = gameObject.transform.GetChild(0).gameObject;
        button.SetActive(false);
        Image i = GetComponent<Image>();
        i.color = new Color(1, 1, 1, 0);
        LeanTween.alpha(i.rectTransform,1,0f)
            .setDelay(2f)
            .setOnComplete(() => { button.SetActive(true); });
	
	}
}
