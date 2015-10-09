using UnityEngine;
using System.Collections;

public class LimitTime : MonoBehaviour {

    [SerializeField]
    private float limitTime;
    private float time;
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(limitTime <= time)
        {
            Destroy(gameObject);
        }
	
	}
}
