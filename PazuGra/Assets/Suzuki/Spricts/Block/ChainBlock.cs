using UnityEngine;
using System.Collections;

//ゲームメインのシステムになるのならば、BlockInfoに

public class ChainBlock : MonoBehaviour {

    private BlockInfo mMyInfo;
    private BlockManager mBlockMgr;

	// Use this for initialization
	void Start () {
        mMyInfo = transform.GetComponent<BlockInfo>();
        mBlockMgr = GameManager.GetInstanc.GetBlockManager();
	}

    //接触したオブジェクトの色を判定
    private bool ColorChack(ColorState state)
    {
        if (state == mMyInfo.m_ColorState) return true;
        return false;
    }

    private ColorState GetState(Collision2D col)
    {
        return col.transform.GetComponent<BlockInfo>().m_ColorState;
    }

    //理想は再起処理をする
    //接触したブロックから先を調べていく。
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Block")){

            if (col.transform.GetComponent<PolygonCollider2D>().gameObject.name != this.gameObject.name){
                //Debug.Log("Remove");
                //Debug.Log(col.gameObject.name);
                //Debug.Log(col.transform.GetComponent<PolygonCollider2D>().gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.CompareTag("Block"))
        {
            if (col.transform.GetComponent<PolygonCollider2D>().gameObject.name != this.gameObject.name)
            {
               // Debug.Log("Remove");
               // Debug.Log(col.gameObject.name);
               // Debug.Log(col.transform.GetComponent<PolygonCollider2D>().gameObject);
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.transform.CompareTag("Block")){
    //        if (GetState(col) == mMyInfo.m_ColorState){
    //            mBlockMgr.AddChainList(GetState(col),col.gameObject);
    //        }

    //        if (mBlockMgr.GetColorCount(GetState(col)) >= 3){
    //            Debug.Log("Remove");
    //            mBlockMgr.RemoveChainList(GetState(col));
    //        }
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D col)
    //{
    //    if (col.transform.CompareTag("Block")){
    //        if (GetState(col) == mMyInfo.m_ColorState){
    //            mBlockMgr.RemoveChainList(GetState(col), col.gameObject);
    //        }
    //    }
    //}

}
