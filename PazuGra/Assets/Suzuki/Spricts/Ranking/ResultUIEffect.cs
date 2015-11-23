using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using uTools;
//UIの項目がどんどん表示されていくように作っていく。
//項目に対してどんどんレーダーカーソルを合わせて表示していく感じでいこう。

public class ResultUIEffect : MonoBehaviour
{

    private GameObject mLeader;
    private List<GameObject> mTargtList = new List<GameObject>();
    private List<GameObject> mValues = new List<GameObject>();
    private Vector2 mStartPoint;
    private GameObject mTarget;
    private GameObject mValue;

    [SerializeField]
    [Range(0.1f, 3.0f)]
    private float mDelay = 0.5f;

    private void InitSearchTarget()
    {
        mLeader = GameObject.Find("Loader_3");
        mTargtList = new List<GameObject>();
        mValues = new List<GameObject>();
        foreach (Transform i in GameObject.Find("Contents").transform) mTargtList.Add(i.gameObject);
        foreach (Transform i in GameObject.Find("Values").transform) mValues.Add(i.gameObject);
    }

    //レーダーを初期位置へ持っていく(Score部分)
    private void InitLeader()
    {
        mStartPoint = mLeader.GetComponent<RectTransform>().position;
    }

    //指定された時間で表示する。
    private void TargetValue(float time)
    {
        Image image = mTarget.GetComponent<Image>();
        image.fillAmount += time;
    }

    private void TargetColorValue(float time)
    {
        Text image = mValue.GetComponent<Text>();
        image.color = new Color(1, 1, 1, time);
    }

    //とりあえず、レーダーの位置をScoreの上に持っていってみる。
    private void MoveTarget(GameObject target, Vector2 to, float time, float dilay = 0.0f)
    {
        //uTweenPosition point = uTweenPosition.Begin(target, from, to, time, dilay) as uTweenPosition;
        //point.Play();
        //Debug.Log(LeanTween.isTweening(target));
        LeanTween.move(target, to, time);
    }

    private IEnumerator DrowUI()
    {
        float delay = mDelay;
        for (int i = 0; i < 4; i++)
        {
            //項目へ移動
            MoveTarget(mLeader, mTargtList[i].transform.position, delay);
            yield return new WaitForSeconds(delay);
            //スプライトを徐々に表示する。
            mTarget = mTargtList[i];
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", delay / 2.0f, "onupdate", "TargetValue"));
            //結果へ移動
            MoveTarget(mLeader, mValues[i].transform.position, delay);
            //少しだけ拡縮する。
            iTween.ScaleTo(mLeader, iTween.Hash("x", 1, "y", 1, "time", 0.2f));
            yield return new WaitForSeconds(0.2f);
            iTween.ScaleTo(mLeader, iTween.Hash("x", 0.7, "y", 0.7, "time", 0.2f));
            //透明度の更新
            mValue = mValues[i];
            iTween.ValueTo(gameObject, iTween.Hash("from", 0, "to", 1, "time", delay / 2.0f, "delay", delay / 2.0f, "onupdate", "TargetColorValue"));
            yield return new WaitForSeconds(delay);
        }
        MoveTarget(mLeader, mStartPoint, delay);

        GameObject.Find("Button_Title").GetComponent<Button>().interactable = true;
        GameObject.Find("Button_Retry").GetComponent<Button>().interactable = true;
    }

    //Effectが終了した項目からスコアを反映させていく。
    private void TargetTextUpdate(GameObject target, string text)
    {
        target.GetComponent<Text>().text = text;
    }

    //score
    //chain
    //combo
    //Drow回数
    private void InitScore()
    {
        //値の反映
        TargetTextUpdate(mValues[0], GameManager.GetInstanc.GetScoreUI().GetScore().ToString());
        TargetTextUpdate(mValues[1], GameObject.Find("GamaManager").GetComponent<ChainManager>().GetMaxChain().ToString());
        TargetTextUpdate(mValues[2], "3");
        TargetTextUpdate(mValues[3], GameManager.GetInstanc.GetRanking().mDrowCount.ToString());
    }

    //Rankingを表示した際に使う
    public void StartEffect()
    {
        GameObject.Find("Button_Title").GetComponent<Button>().interactable = false;
        GameObject.Find("Button_Retry").GetComponent<Button>().interactable = false;
        InitSearchTarget();
        InitLeader();
        InitScore();
        StartCoroutine(DrowUI());
    }

    // Use this for initialization
    void Start()
    {
        //InitSearchTarget();
        //InitLeader();

        //StartCoroutine(DrowUI());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Time.timeScale = 3.0f;
        if (Input.GetMouseButtonUp(0)) Time.timeScale = 1.0f;
    }
}
