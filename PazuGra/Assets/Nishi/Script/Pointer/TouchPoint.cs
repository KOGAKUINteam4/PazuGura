using UnityEngine;
using System.Collections;

public class TouchPoint : MonoBehaviour {

    [SerializeField]
    private GameObject touchParticle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)) AudioManager.Instance.SEPlay(AudioList.touch);
        if (Input.GetMouseButton(0))
        {
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            screenPos.z = 0;
            GameObject temp = (GameObject)Instantiate(touchParticle, screenPos,Quaternion.Euler(-90,0,0));
            temp.transform.SetParent(GameObject.Find("PuauseUI").transform,true);
        }
	}
}
