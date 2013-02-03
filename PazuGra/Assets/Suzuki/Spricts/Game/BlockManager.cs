using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//ブロック管理クラス

public class BlockManager : MonoBehaviour {

    //存在するブロックリスト
    private List<BlockInfo> mBlockList;
    private List<GameObject>[] mBlocks = new List<GameObject>[5];

    public BlockManager()
    {
        mBlockList = new List<BlockInfo>();
        for (int i = 0; i < 5; i++) mBlocks[i] = new List<GameObject>();
    }

    public List<BlockInfo> GetBlockList()
    {
        return mBlockList;
    }

    public void AddBlock(BlockInfo info)
    {
        mBlockList.Add(info);
    }


    //いらなくなりそう。

    //対象のカラーのブロックリストへ追加
    public void AddChainList(ColorState state,GameObject target)
    {
        mBlocks[(int)state].Add(target);
    }

    //登録されている数の更新
    public int GetColorCount(ColorState state)
    {
        return mBlocks[(int)state].Count;
    }

    //リストの削除更新
    public void RemoveChainList(ColorState state,GameObject target)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var i in mBlocks[(int)state]){
            if (target != i) list.Add(i);
        }
        mBlocks[(int)state] = new List<GameObject>(list);
    }

    public void DeleteBlockList(ColorState state)
    {
        mBlocks[(int)state].Clear();
    }

    public void RemoveChainList(ColorState state)
    {
        mBlocks[(int)state].Clear();
    }

}
