using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StopUI : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.GetInstanc.GetTutorial())
        {
            GetComponent<Image>().enabled = false;
        }
        else
        {
            GetComponent<Image>().enabled = true;
        }
    }
}
