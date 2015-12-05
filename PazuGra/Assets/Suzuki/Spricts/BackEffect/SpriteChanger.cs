using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpriteChanger : MonoBehaviour {

    [SerializeField]
    private Sprite[] mSprites;

    private Image mImage;
    private int mIndex;

	// Use this for initialization
	void Start () {
        mImage = GetComponent<Image>();
	}

    public float speed = 0.01f;			// スクロールするスピード
    [SerializeField]
    private float mAngle;

    void Update()
    {
        mAngle += Time.deltaTime * speed;
        transform.rotation = Quaternion.AngleAxis(mAngle,new Vector3(0,0,1));
        //float scroll = Mathf.Repeat(Time.time * speed, 1);		// 時間によってYの値が0から1に変化していく.1になったら0に戻り繰り返す.
        //Vector2 offset = new Vector2(scroll, 0);				// Xの値がずれていくオフセットを作成.
        //GetComponent<Image>().material.SetTextureOffset("_MainTex", offset);	// マテリアルにオフセットを設定する.
    }
}
