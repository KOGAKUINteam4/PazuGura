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

    private void Start()
    {
        mFactory = GameObject.Find("Factory").GetComponent<BlockFactory>();
        mStampCounter = GameObject.Find("StampTemplate").GetComponent<TemplateStamp>();
    }

    private bool IsActive()
    {
        //コストが
        if (mStampCounter.GetCost() >= 0){
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (mStampCounter.GetCost() == 0) GetComponent<Button>().interactable = false;
        else GetComponent<Button>().interactable = true;
    }

    public void ChackButtonActive()
    {
        mStampCounter.AddCost(-mCounter);
        //ボタンの無効
        if (!IsActive()) { mStampCounter.Init(); return; }
        if (GameObject.Find("Factory").GetComponent<BlockFactory>().GetShoot()) return;
        if (mPoint == null || mPoint.Length == 0) mFactory.DebugStart(mName);
        else mFactory.StampUpdate(mPoint);
    }

    public void StampUpdate(Vector2[] point)
    {
        mPoint = point;
        //mFactory.StampUpdate(point);
    }


}
