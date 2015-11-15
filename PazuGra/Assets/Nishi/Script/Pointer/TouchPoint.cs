using UnityEngine;
using System.Collections;

public class TouchPoint : MonoBehaviour {

    [SerializeField]
    private GameObject touchParticle;

	// Use this for initialization
	void Start () {
        Debug.Log("start");
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenPos.z = 0;
            Instantiate(touchParticle, screenPos,Quaternion.Euler(-90,0,0));
        }
	}
}
