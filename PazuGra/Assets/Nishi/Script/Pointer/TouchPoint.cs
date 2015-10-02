using UnityEngine;
using System.Collections;

public class TouchPoint : MonoBehaviour {

    public GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenPos.z = 0;
            Instantiate(obj, screenPos,Quaternion.Euler(-90,0,0));
        }
	}
}
