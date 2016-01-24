using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//6桁の桁数で作る。
public class ScoreUI : MonoBehaviour {

    [SerializeField]
    private Sprite[] mSprite = new Sprite[10];
    private GameObject[] mCounter = new GameObject[6];
    [SerializeField]
    private GameObject[] mScoreCounter = new GameObject[4];
    //スコア
    [SerializeField]
    private float mSocre = 0;

    [SerializeField]
    private GameObject mTarget;
    [SerializeField]
    private GameObject mScoreParent;

	// Use this for initialization
	void Start () {
        Init();
	}

    //GameMainをはじめた際に使う。
    public void InitScore()
    {
        mSocre = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddScore(700);
            UpdateScore(700);
            CreateEffect();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject rank = Instantiate(Resources.Load("Prefub/RankingSet")) as GameObject;
            rank.transform.SetParent(GameManager.GetInstanc.GetUIContller().SearchParent(Layers.Layer_Front).transform,false);
            rank.transform.GetChild(0).GetComponent<ResultUIEffect>().StartEffect();
        }
    }

    public void Init()
    {
        int counter = 0;
        foreach(Transform i in gameObject.transform){
            mCounter[counter] = i.gameObject;
            counter++;
        }
        counter = 0;
        if (transform.name == "RankScore") return;
        foreach (Transform i in mTarget.transform){
            mScoreCounter[counter] = i.gameObject;
            counter++;
        }
        //mScoreParent = GameObject.Find("Score");
        //mScoreParent.SetActive(false);
    }

    public void AddScore(float score)
    {
        mSocre += score;
        //Debug.Log(score);
    }

    //一括の処理
    public void ScoreEvent(float score)
    {
        AddScore(score);
        UpdateScore(score);
    }

    public int GetScore()
    {
        Debug.Log("Score : "+mSocre);
        return (int)mSocre;
    }

    //1から?
    public void UpdateScore(float score)
    {
        for (int i = 0; i < 4; i++){
            mScoreCounter[i].GetComponent<Image>().sprite = GetScoreSprite(i+2,score);
        }
        for (int i = 0; i < 6; i++){
            SetNumber(i);
        }
    }

    public void UpdateScore()
    {
        for (int i = 0; i < 6; i++)
        {
            SetNumber(i);
        }
    }

    public void CreateEffect()
    {
        mScoreParent.SetActive(true);
        // 現在位置からx:-20までランダムに揺れる
        iTween.ShakePosition(mScoreParent, 
            iTween.Hash("y", -0.1, "islocal", false, "time", 1.0f, "oncomplete", "NotActive","oncompletetarget", this.gameObject));
    }

    private void NotActive()
    {
        mScoreParent.SetActive(false);
    }

    //桁数に応じた数字のスプライトを返す。
    private Sprite GetScoreSprite(int score,float value)
    {
        int scoreValue = (int)value;
        string length = scoreValue.ToString();
        for (int i = length.Length; i < 6; i++) { length = "0" + length;}
        return mSprite[int.Parse(length.Substring(score, 1))];
        //return mSprite[int.Parse(length.Substring(score-1, 1))];
    }

    public void SetNumber(int num)
    {
        //Debug.Log("更新している桁 : "+num);
        //最後の桁が取りきれていない説
        mCounter[num].GetComponent<Image>().sprite = GetScoreSprite(num, mSocre);
        //mCounter[num - 1].GetComponent<Image>().sprite = GetScoreSprite(num,mSocre);
    }
}
