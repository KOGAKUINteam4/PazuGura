using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager mInstance;
    //ブロック管理
    private BlockManager mBlockManager;
    //レイヤー内のUI管理
    private UIContller mUIContller;

    private PhpContact mPhp;

    [SerializeField]
    private ScoreUI mScore;
    private RankingParam mRanking;

    private bool isTutorial;

    private void Awake()
    {
        mBlockManager = gameObject.AddComponent<BlockManager>();
        mUIContller = gameObject.AddComponent<UIContller>();
        mPhp = gameObject.AddComponent<PhpContact>();
        mRanking = gameObject.AddComponent<RankingParam>();
    }

    public static GameManager GetInstanc
    {
        get { if (mInstance == null)mInstance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager; return mInstance; }
    }

    public ScoreUI GetScoreUI()
    {
        //mScore = GameObject.Find("ScoreParent").GetComponent<ScoreUI>();
        return mScore;
    }

    public PhpContact GetPhpContact()
    {
        return mPhp;
    }

    public BlockManager GetBlockManager()
    {
        return mBlockManager;
    }

    public UIContller GetUIContller()
    {
        return mUIContller;
    }

    public RankingParam GetRanking()
    {
        return mRanking;
    }

    public void SetTutorial(bool can)
    {
        isTutorial = true;
    }

    public bool GetTutorial()
    {
        return isTutorial;
    }
}
