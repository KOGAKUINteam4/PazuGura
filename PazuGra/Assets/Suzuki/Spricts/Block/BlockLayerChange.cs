using UnityEngine;
using System.Collections;

public class BlockLayerChange : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 12){
            gameObject.layer = 9;
            Destroy(this);
        }
    }


}
