using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Arrow : MonoBehaviour {

    BlockFactory mFactory;
    

    // Use this for initialization
    void Start () {
        mFactory = GameObject.Find("Factory").GetComponent<BlockFactory>();
    }
	
	// Update is called once per frame
	void Update () {
        if(mFactory.GetShoot())
        {
            GetComponent<Image>().enabled = true;
            GetComponent<Animator>().enabled = true;
        }
        else
        {
            GetComponent<Image>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }
	}
}
