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
    [ContextMenu("Update")]
    public void Init()
    {
        mGameManager = GameManager.GetInstanc;
        mPhp = mGameManager.GetPhpContact();
        mPhp.DateBaseUpdate(30000);
        //mPhp.DateBaseUpdate(GameManager.GetInstanc.GetScoreUI().GetScore());
        mPhp.UpdateRankingDate(ResultDate, UpdateViewValue);
    }
	
    //まだ使っていない
    //自分のスコアと一致したら赤くする
    public bool RankerColor(int score)
    {
        //GameObject target = mScoreParent;
        //if (target == null) target = GameObject.Find("mScoreParent");
        //if (target == null) return false;
        //Score_Value
        if (GameObject.Find("Score_Value").GetComponent<Text>().text == score.ToString()) return true;
        //if (target.GetComponent<ScoreUI>().GetScore() == score) return true;
        return false;
    }

    //Viewないのスコアの更新
    [ContextMenu("Debug")]
    private void UpdateViewValue()
    {
        mResult.Sort();
        mResult.Reverse();
        GameObject target = GameObject.Find("RankContents");
        for (int i = 1; i < 16; i++)
        {
            if (mResult.Count - 1 == i) break;
            target.transform.GetChild(i).GetChild(0).GetComponent<Text>().text =mResult[i].ToString();
            if (RankerColor(mResult[i])) target.transform.GetChild(i).GetChild(0).GetComponent<Text>().color = Color.yellow;
            //target.transform.GetChild(i).GetChild(0).GetComponent<ScoreUI>().AddScore(mResult[i]);
            //target.transform.GetChild(i).GetChild(0).GetComponent<ScoreUI>().UpdateScore();
        }
    }

    //ランキングの作成
    private void ResultDate()
    {
        mResult = mPhp.GetRankingDate();
        //mResult.Reverse();
    }

}
