using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class NishiUIPixRange : MonoBehaviour {

    [Range(0, 1)]
    public float range;
    private Material mat;
	// Use this for initialization
	void Start () {
        mat = GetComponent<Image>().material;
	}
	
	// Update is called once per frame
	void Update () {
        mat.SetFloat("_Range", range);
        transform.parent.transform.GetComponent<Image>().color = new Color(1,1,1,1-range);
	}
}
