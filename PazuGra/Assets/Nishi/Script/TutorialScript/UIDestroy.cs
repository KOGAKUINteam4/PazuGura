using UnityEngine;
using System.Collections;

public class UIDestroy : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {
        if(TutorialManager.Instance.GetStep() >= 11)
        {
            Destroy(gameObject);
        }
	}
}
