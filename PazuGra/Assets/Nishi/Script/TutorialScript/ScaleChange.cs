using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Image i = GetComponent<Image>();
        i.color = new Color(1, 1, 1, 0);
        LeanTween.alpha(i.rectTransform,1,0f).setDelay(2f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
