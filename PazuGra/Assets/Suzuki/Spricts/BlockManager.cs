using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//ブロック管理クラス

public class BlockManager : MonoBehaviour {

    private List<BlockInfo> mBlockList;

    public BlockManager()
    {
        mBlockList = new List<BlockInfo>();
    }

    public List<BlockInfo> GetBlockList()
    {
        return mBlockList;
    }

    public void AddBlock(BlockInfo info)
    {
        mBlockList.Add(info);
    }

}
