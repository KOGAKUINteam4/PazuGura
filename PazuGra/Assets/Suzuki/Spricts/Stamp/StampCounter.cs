using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StampCounter : MonoBehaviour {

    [SerializeField]
    private int mCounter;

    private bool IsActive()
    {
        if (mCounter == 0){
            return true;
        }
        return false;
    }

    public void ChackButtonActive()
    {
        if (GameObject.Find("Factory").GetComponent<BlockFactory>().GetShoot()) return;
        mCounter--;
        if (IsActive()) GetComponent<Button>().interactable = false;
    }


}
