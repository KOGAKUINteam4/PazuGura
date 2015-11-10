using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingSeq : MonoBehaviour {

    private GameManager mGameManager;
    private PhpContact mPhp;
    private List<int> mResult = new List<int>();
	// Use this for initialization
	void Start () {
        mGameManager = GameManager.GetInstanc;
        mPhp = mGameManager.GetPhpContact();
        mPhp.UpdateRankingDate(ResultDate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //自分のスコアと一致したら赤くする
    private bool RankerColor(int score)
    {
        GameObject target = GameObject.Find("ScoreParent");
        if (target == null) return false;
        if (target.GetComponent<ScoreUI>().GetScore() == score) return true;
        return false;
    }

    //Viewないのスコアの更新
    [ContextMenu("Debug")]
    private void UpdateViewValue()
    {
        GameObject target = GameObject.Find("RankContents");
        for (int i = 0; i < 15; i++)
        {
            if (mResult.Count - 1 == i) break;
            //Debug.Log(target.transform.GetChild(i).name);
            //Debug.Log(target.transform.GetChild(i).GetChild(0));
            target.transform.GetChild(i).GetChild(0).GetComponent<ScoreUI>().AddScore(mResult[i]);
            target.transform.GetChild(i).GetChild(0).GetComponent<ScoreUI>().UpdateScore();
        }
    }

    //ランキングの作成
    private void ResultDate()
    {
        mResult = mPhp.GetRankingDate();
        mResult.Reverse();
    }

}
