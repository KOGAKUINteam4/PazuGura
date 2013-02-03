using UnityEngine;
using System.Collections;

public class DebugMath : MonoBehaviour {

    [SerializeField]
    private int mCounter;

	// Use this for initialization
	void Start () {

        MathDebug();
	}

    private void MathDebug()
    {
        mCounter %= 10;
        Debug.Log(mCounter);

        mCounter -= 5;

        if (mCounter == 0) return;

        if (mCounter <= 0)
        {
            Debug.Log(mCounter * (-1));
        }
        else

        Debug.Log(5 - mCounter);

        //3 2
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
