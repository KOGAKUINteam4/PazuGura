using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager mInstance;
    //ブロック管理
    private BlockManager mBlockManager;
    //レイヤー内のUI管理
    private UIContller mUIContller;

    private PhpContact mPhp;

    private ScoreUI mScore;

    private void Awake()
    {
        mBlockManager = gameObject.AddComponent<BlockManager>();
        mUIContller = gameObject.AddComponent<UIContller>();
        mPhp = gameObject.AddComponent<PhpContact>();
    }

    public static GameManager GetInstanc
    {
        get { if (mInstance == null)mInstance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager; return mInstance; }
    }

    public ScoreUI GetScoreUI()
    {
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
}
