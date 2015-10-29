using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainDelete : MonoBehaviour
{
    private bool m_listIn = false; //リストに入ったか 
    private bool m_isDead = false; //自身の消えるフラグ

    private List<GameObject> otherBlocks;

    private BlockInfo m_MyInfo;

    public void Start()
    {
        m_MyInfo = gameObject.transform.parent.GetComponent<BlockInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = gameObject.transform.parent.position;
        if (m_isDead)
        {
            if (!m_listIn)
            {
                GameObject.Find("GamaManager").GetComponent<DeadManager>().PushToList(gameObject.transform.parent.gameObject); //自分をを消すリストへ
                m_listIn = true;
                gameObject.transform.parent.GetComponent<Rigidbody2D>().WakeUp();

            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //自分がリストに入っている
        if (m_listIn)
        {

            //ブロックでなければリターン
            if (other.tag != "Block" && other.name == gameObject.gameObject.transform.parent.name) 
            {
                return;
            }

            //ブロックタグでありなおかつ同じ色
            if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
            {
                other.gameObject.transform.FindChild("Collider").gameObject.GetComponent<ChainDelete>().DeadFlagOn();
                
            }
        }
    }


    //自身が死亡する
    public void DeadFlagOn()
    {
        m_isDead = true;
    }

    //消える予定か？
    public bool IsClear()
    {
        return m_isDead;
    }

    //消えないとわかったら使う
    public void FlagClear()
    {
        m_listIn = false;
        m_isDead = false;
    }

    //接触したオブジェクトの色を判定
    private bool ColorChack(ColorState state)
    {
        if (state == m_MyInfo.m_ColorState) return true;
        return false;
    }

}