using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Chain : MonoBehaviour
{


    private BlockInfo m_MyInfo;

    /// <summary>
    /// true:チェック済み false::チェックされてない;
    /// </summary>
    private bool m_isCheck = false;
    private List<GameObject> m_Chains = new List<GameObject>();  //色が変化する予定のグループ
    private bool m_isFlash = false;  //色の変更されるべきか?
    private GameObject m_ActiveParticle = null; //アクティブなパーティクルが入っているか

    [SerializeField]
    private GameObject m_ChainParticle;


    // Use this for initialization
    void Start()
    {
        m_ChainParticle = (GameObject)Resources.Load("Prefub/ChainParticle");
        m_MyInfo = gameObject.transform.parent.GetComponent<BlockInfo>();

    }

    private Color ColorTable()
    {
        if (m_MyInfo.m_ColorState == ColorState.Color_BLUE)        return Color.blue;
        else if (m_MyInfo.m_ColorState == ColorState.Color_YELLOW) return Color.yellow;
        else if (m_MyInfo.m_ColorState == ColorState.Color_GREEN)  return Color.green;
        else if (m_MyInfo.m_ColorState == ColorState.Color_RED)    return Color.red;
        else return Color.black;
    } 

    // Update is called once per frame
    void Update()
    {
        if (m_isCheck || m_isFlash)
        {
            gameObject.transform.parent.GetComponent<Rigidbody2D>().WakeUp();
        }
        if (m_isFlash)
        {
            if (!m_Chains.Contains(transform.parent.gameObject)) m_Chains.Add(transform.parent.gameObject);
        }
        
        //色の変更の処理
        if (m_Chains.Count >= 3)
        {
            if(m_ActiveParticle == null)
            {
                m_ActiveParticle = (GameObject)Instantiate(m_ChainParticle, transform.position, Quaternion.identity);
                m_ActiveParticle.transform.SetParent(transform.parent);
            }
        }
        else
        {
            //transform.parent.transform.GetChild(0).GetComponent<Image>().color = ColorTable();
            Destroy(m_ActiveParticle);
        }

        //NULLがあれば削除
        if(m_Chains.Contains(null))
        {
            m_Chains.Remove(null);
        }

        


    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (m_isCheck)
        {
            if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
            {
                var temp = other.transform.FindChild("Collider");
                if (!temp.GetComponent<Chain>().IsCheck())
                {
                    //this.m_isFlash = true;
                    //temp.GetComponent<Chain>().m_isFlash = true;
                    //temp.GetComponent<Chain>().m_Chains = this.m_Chains;
                    temp.GetComponent<Chain>().SetCheck(true);
                }
            }
        }

        if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
        {
            var temp = other.transform.FindChild("Collider");
            //this.m_isFlash = true;
            temp.GetComponent<Chain>().m_isFlash = true;

            //光る予定のない物は削除
            List<GameObject> temps = m_Chains;
            foreach (GameObject obj in m_Chains)
            {
                if (!obj.transform.GetChild(1).GetComponent<Chain>().m_isFlash)
                {
                    temps.Remove(obj);
                }
            }

            m_Chains = temps;
            temp.GetComponent<Chain>().m_Chains = this.m_Chains;
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
        {
            m_Chains.Clear();
            foreach (GameObject obj in m_Chains)
            {
                if(obj != transform.parent.gameObject)
                obj.transform.GetChild(1).GetComponent<Chain>().m_Chains = m_Chains;
            }
        }
    }

    public bool IsCheck()
    {
        return m_isCheck;
    }

    public void SetCheck(bool check)
    {
        m_isCheck = check;
    }
    //接触したオブジェクトの色を判定
    private bool ColorChack(ColorState state)
    {
        if (m_MyInfo.m_ColorState == ColorState.Color_ALL) return true;
        if (state == m_MyInfo.m_ColorState || state == ColorState.Color_ALL) return true;
        return false;
    }
}
