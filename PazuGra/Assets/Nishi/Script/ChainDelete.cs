using UnityEngine;
using System.Collections;

public class ChainDelete : MonoBehaviour
{
    public bool m_isDead = false; //自身の消えるフラグ
    public bool m_listIn = false;//リストに入ったか
    private BlockInfo m_MyInfo;

    public void Start()
    {
        m_MyInfo = gameObject.transform.parent.GetComponent<BlockInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = gameObject.transform.parent.position;
        if (m_listIn)
        {
            if (!m_isDead)
            {
                Debug.Log("list in " + gameObject.transform.parent.gameObject.name);
                GameObject.Find("GamaManager").GetComponent<DeadManager>().PushToList(gameObject.transform.parent.gameObject); //自分をを消すリストへ
                m_isDead = true;
                gameObject.transform.parent.GetComponent<Rigidbody2D>().WakeUp();
            }
            

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("name " + other.name);
        Debug.Log("tags " + other.tag);

        //自分がリストに入っている
        if (m_isDead)
        {

            //ブロックでなければリターン
            if (other.tag != "Block" && other.name == gameObject.gameObject.transform.parent.name) 
            {
                return;
            }

            Debug.Log("name " + other.name);
            Debug.Log("tags " + other.tag);

            //ブロックタグでありなおかつ同じ色
            if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
            {
                Debug.Log("log " + other.tag);
                other.gameObject.transform.FindChild("Collider").gameObject.GetComponent<ChainDelete>().ListInFlagOn();
                
            }
        }
    }


    //自身が死亡する
    public void ListInFlagOn()
    {
        Debug.Log("on");
        m_listIn = true;
    }

    //消える予定か？
    public bool IsClear()
    {
        return m_listIn;
    }

    //消えないとわかったら使う
    public void ClearFlag()
    {
        m_isDead = false;
        m_listIn = false;
    }

    //接触したオブジェクトの色を判定
    private bool ColorChack(ColorState state)
    {
        if (state == m_MyInfo.m_ColorState) return true;
        return false;
    }

}
