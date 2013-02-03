using UnityEngine;
using System.Collections;

public class DebugPrintSprict : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [ContextMenu("Position")]
    private void PinrtDebug()
    {
        Debug.Log("Position : "+transform.localPosition);
    }

}
