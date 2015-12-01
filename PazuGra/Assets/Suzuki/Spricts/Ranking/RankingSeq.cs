using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class RankingSeq : MonoBehaviour {

    private GameManager mGameManager;
    private PhpContact mPhp;
    private List<int> mResult = new List<int>();

    [SerializeField]
    private GameObject mScoreParent;

	// Use this for initialization
	void Start () {
        mGameManager = GameManager.GetInstanc;
        mPhp = mGameManager.GetPhpContact();
        mPhp.DateBaseUpdate(GameManager.GetInstanc.GetScoreUI().GetScore());
        mPhp.UpdateRankingDate(ResultDate, UpdateViewValue);
	}

    //Rankingが呼ばれた際に使う。
    public void Init()
    {
        mGameManager = GameManager.GetInstanc;
        mPhp = mGameManager.GetPhpContact();
        mPhp.DateBaseUpdate(GameManager.GetInstanc.GetScoreUI().GetScore());
        mPhp.UpdateRankingDate(ResultDate, UpdateViewValue);
    }
	
    //まだ使っていない
    //自分のスコアと一致したら赤くする
    public bool RankerColor(int score)
    {
        GameObject target = mScoreParent;
        if (target == null) target = GameObject.Find("mScoreParent");
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
            target.transform.GetChild(i).GetChild(0).GetComponent<Text>().text =mResult[i].ToString();
            //target.transform.GetChild(i).GetChild(0).GetComponent<ScoreUI>().AddScore(mResult[i]);
            //target.transform.GetChild(i).GetChild(0).GetComponent<ScoreUI>().UpdateScore();
        }
    }

    //ランキングの作成
    private void ResultDate()
    {
        mResult = mPhp.GetRankingDate();
        mResult.Reverse();
    }

}
