using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager mInstance;
    //ブロック管理
    private BlockManager mBlockManager;
    //レイヤー内のUI管理
    private UIContller mUIContller;

    private void Awake()
    {
        mBlockManager = gameObject.AddComponent<BlockManager>();
        mUIContller = gameObject.AddComponent<UIContller>();
    }

    public static GameManager GetInstanc
    {
        get { if (mInstance == null)mInstance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager; return mInstance; }
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
