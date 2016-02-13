using UnityEngine;
using System.Collections;

public class Dummys : MonoBehaviour {
    	
	// Update is called once per frame
	void Update ()
    {
        if(!GameManager.GetInstanc.GetTutorial())
        {
            Destroy(gameObject);
        } 	
	}
}
