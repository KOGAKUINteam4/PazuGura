using UnityEngine;
using System.Collections;

public class ChainDelete : MonoBehaviour
{
    private bool m_isDead = false; //自身の消えるフラグ
    private bool m_isClear = false;//すでに消える予定であるか？
    private BlockInfo m_MyInfo;

    public void Start()
    {
        m_MyInfo = GetComponent<BlockInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        //消えるフラグがたったら
        if (m_isDead)
        {

            //タグがブロックのオブジェクトを探す
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Block"))
            {
                //消されるされたブロックから距離が1,0f以下なら
                float dit = Vector2.Distance(gameObject.transform.position, obj.gameObject.transform.position);
                if (dit < 1.0f && ColorChack(obj.GetComponent<BlockInfo>().m_ColorState))
                {

                    if (!obj.gameObject.GetComponent<ChainDelete>().IsClear())
                    {
                        obj.GetComponent<ChainDelete>().DeadFlagOn();//相手の消えるフラグon
                        GameObject.Find("GameManager").GetComponent<DeadManager>().PushToList(obj); //相手を消すリストへ
                        GameObject.Find("GameManager").GetComponent<DeadManager>().PushToList(gameObject); //自分をを消すリストへ
                        m_isClear = true;
                    }
                }
            }
            m_isDead = false;

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
        return m_isClear;
    }

    //接触したオブジェクトの色を判定
    private bool ColorChack(ColorState state)
    {
        if (state == m_MyInfo.m_ColorState) return true;
        return false;
    }

}
