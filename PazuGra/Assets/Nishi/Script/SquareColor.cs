using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SquareColor : MonoBehaviour {

    Image square;
    BlockFactory mFactory;

	// Use this for initialization
	void Start () {
        square = transform.GetChild(0).GetComponent<Image>();
        mFactory = GameObject.Find("Factory").GetComponent<BlockFactory>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if(mFactory.isRainbow)
        {
            square.color = new Color(1, 1, 1, 1);
        }
        else
        {
            square.color = new Color(1, 1, 1, 0);
        }
	
	}
}
