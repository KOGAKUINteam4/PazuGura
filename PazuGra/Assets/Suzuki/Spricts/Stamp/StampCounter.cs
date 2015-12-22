using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TemplateStampPoint;
public class StampCounter : MonoBehaviour {

    [SerializeField][Range(1,5)]
    private int mCounter;

    private TemplateStamp mStampCounter;
    private BlockFactory mFactory;

    private Vector2[] mPoint;

    [SerializeField]
    private string mName;

    [SerializeField]
    private ColorState mColor;

    private void Start()
    {
        mFactory = GameObject.Find("Factory").GetComponent<BlockFactory>();
        mStampCounter = GameObject.Find("StampTemplate").GetComponent<TemplateStamp>();
    }

    private bool IsActive()
    {
        //コストが
        if (mStampCounter.GetCost() -mCounter >= 0){
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (mStampCounter.GetCost() - mCounter < 0) GetComponent<Button>().interactable = false;
        else GetComponent<Button>().interactable = true;
    }

    public void ChackButtonActive()
    {
        
        //ボタンの無効
        if (!IsActive()) { return; }
        if (GameObject.Find("Factory").GetComponent<BlockFactory>().GetShoot()) return;

        AudioManager.Instance.SEPlay(AudioList.Stamp);
        mStampCounter.AddCost(-mCounter);

        if (mPoint == null || mPoint.Length == 0) mFactory.DebugStart(mName,(int)mColor);
        else mFactory.StampUpdate(mPoint, (int)mColor);
    }

    public void StampUpdate(Vector2[] point)
    {
        mPoint = point;
        //mFactory.StampUpdate(point);
    }

    //全てのスタンプの初期化
    //Iconも
    [ContextMenu("StampInit")]
    public void InitStamp()
    {
        InitPoint();
        //CircleStamp,SixStamp,starStamp,DebugStamp,ArtStamp
        string[] name = new string[5] { "CircleStamp", "SixStamp", "starStamp", "DebugStamp", "ArtStamp" };
        GameObject[] target = new GameObject[5];
        for (int i = 0; i < 5; i++)
        {
            target[i] = transform.parent.GetChild(i + 1).gameObject;
            target[i].GetComponent<StampCounter>().InitPoint();
            //SpriteがLoad失敗している?
            Texture2D sprite = Resources.Load("Stamp/" + name[i]) as Texture2D;
            //Debug.Log("Stamp/" + name[i] + " : "+ sprite);
            target[i].transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(sprite,new Rect(0,0,256,256),Vector2.zero); 
        }
    }

    private void InitPoint()
    {
        mPoint = null;
    }

}
