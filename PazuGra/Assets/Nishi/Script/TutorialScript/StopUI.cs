using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StopUI : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetInstanc.GetTutorial() && TutorialManager.Instance.GetStep() < 11) 
        {
            GetComponent<Image>().enabled = true;
        }
        else
        {
            GetComponent<Image>().enabled = false;
        }
    }
}
