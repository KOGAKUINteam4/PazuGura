using UnityEngine;
using System.Collections;

public class BlockLayerChange : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == 13)
        {
            gameObject.layer = 11;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 12){
            gameObject.layer = 9;
            //Destroy(this);
        }
    }


}
