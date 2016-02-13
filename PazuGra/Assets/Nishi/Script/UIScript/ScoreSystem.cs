using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
    [SerializeField]
    private GameObject showSprite;  // スプライト表示用オブジェクト(プレハブ)

    public Sprite[] nums;
    private GameObject[] numSpriteGird;         // 表示用スプライトオブジェクトの配列
    private float width;
    //private int k = 0;

    public void Draw(int value)
    {
        width = showSprite.GetComponent<RectTransform>().sizeDelta.x;
        string strint = value.ToString();

        Remove();

        // 表示桁数分だけオブジェクト作成
        numSpriteGird = new GameObject[strint.Length];
        int k = 0;
        for (var i = numSpriteGird.Length-1; i >= 0; i--)
        {
            // オブジェクト作成
            numSpriteGird[i] = Instantiate(
                showSprite,
                transform.position + new Vector3((float)i * -width, 0),
                Quaternion.identity) as GameObject;

            // 表示する数値指定
            numSpriteGird[i].GetComponent<Image>().sprite = nums[int.Parse(strint[k].ToString())];

            // 自身の子階層に移動
            numSpriteGird[i].transform.SetParent(transform,false);
            k++;
        }

    }

    public void Remove()
    {
        if (numSpriteGird != null)
        {
            foreach (var numSprite in numSpriteGird)
            {
                GameObject.Destroy(numSprite);
            }
        }
    }
}
