using UnityEngine;
using System.Collections;

//ブロック生成用のクラス

public class BlockFactory : MonoBehaviour {

    private GameManager mGamaManager;
    private BlockManager mBlockMgr;
    private BlockInfo mInfo;

	// Use this for initialization
	void Start () {
        mGamaManager = GameManager.GetInstanc;
        mBlockMgr = mGamaManager.GetBlockManager();
	}

    //画面を押された瞬間
    public void CreateBlockOnTouch()
    {
        mInfo = new BlockInfo();
    }

    //画面を押されている。
    public void CreateBlockOnStay()
    {
        //インフォメーションの更新
    }

    //画面を押され、リリースされたとき
    public void CreateBlockOnRelease()
    {
        //生成された物のインフォ
        mBlockMgr.AddBlock(mInfo);

        //画面上に生成
    }

}
