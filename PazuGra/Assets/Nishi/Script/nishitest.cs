using UnityEngine;
using System.Collections;

public class nishitest : MonoBehaviour {

    private PolygonCollider2D poly;

    // Use this for initialization
    void Start () {
        poly = gameObject.GetComponent<PolygonCollider2D>();

    }
	
	// Update is called once per frame
	void Update () {
        //ポリゴンコリジョン２Dを変更する
        poly.points = new Vector2[]
        {
            new Vector2(1,1),
            new Vector2(2,1),
            new Vector2(1,2),
            new Vector2(2,2),
        };
    }
}
