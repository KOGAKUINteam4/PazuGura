using UnityEngine;
using System.Collections;

//ゲームメインのシステムになるのならば、BlockInfoに

public class ChainBlock : MonoBehaviour {

    private BlockInfo mMyInfo;
    //private BlockManager mBlockMgr;

    [SerializeField]
    private int mChainCounter = 0;
    public bool mChainChack { set; get; }

	// Use this for initialization
	void Start () {
        mMyInfo = transform.GetComponent<BlockInfo>();
        //mBlockMgr = GameManager.GetInstanc.GetBlockManager();
	}

    //接触したオブジェクトの色を判定
    private bool ColorChack(ColorState otherState)
    {
        if (otherState == mMyInfo.m_ColorState) return true;
        return false;
    }

    private ColorState GetState(Collision2D col)
    {
        return col.transform.GetComponent<BlockInfo>().m_ColorState;
    }

    private ColorState GetState(Collider2D col)
    {
        return col.transform.GetComponent<BlockInfo>().m_ColorState;
    }

    //自分と相手を調べた状態にする。
    private void OtherChain(Collision2D col)
    {
        if (IsChainChack(col)){
            //あたったものから先を見る
            if (!col.collider.gameObject.GetComponent<ChainBlock>().IsChainChack(col)){
            }
        }
    }

    private void IsChacked(Collision2D col,bool state)
    {

        col.transform.GetComponent<ChainBlock>().mChainChack = state;
    }

    private bool IsChainChack(Collision2D col)
    {
        if (col.transform.GetComponent<ChainBlock>().mChainChack) return true;
        return false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Block"))
        {
            if (GetState(col) == mMyInfo.m_ColorState){
                mChainCounter++;
                if (mChainCounter >= 2) {mChainCounter = 0; }
            }
        }
    }


    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Block"))
        {
            if (GetState(col) == mMyInfo.m_ColorState)
            {
                mChainCounter--;
            }
        }
    }

}
