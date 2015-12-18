using UnityEngine;
using System.Collections;

public class TitlePlating : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition += new Vector3(800.0f, 0f, 0f) * Time.deltaTime;
        if(transform.localPosition.x > 500 )
        {
            transform.localPosition = new Vector3(-640, transform.localPosition.y, 0f);
        }
	
	}
}
