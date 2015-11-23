using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIPixRange : MonoBehaviour {

    [Range(0, 1)]
    public float range;
    private Material mat;
	// Use this for initialization
	void Start () {
        mat = GetComponent<Image>().material;
        //mat = transform.parent.GetComponent<Image>().material;
	}
	
	// Update is called once per frame
	void Update () {
        mat.SetFloat("_Range", range);
        transform.parent.transform.GetComponent<Image>().color = new Color(1,1,1,1-range);
        Color color = transform.GetComponent<Image>().color;
        //transform.GetComponent<Renderer>().material.SetFloat("",range);
        transform.GetComponent<Image>().color = new Color(color.r,color.g,color.b,1 - range);
    }
}
