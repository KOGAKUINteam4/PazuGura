using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MathFps : MonoBehaviour {

    private Text mText;

	// Use this for initialization
	void Start () {
        mText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        float fps = 1f / Time.deltaTime;
        mText.text = fps.ToString();
	}
}
