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

    public float debugTime;


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

    private bool mIsHit = false;

    // Update is called once per frame
    void Update()
    {
        if (m_isFlash)
        {
            if (!m_Chains.Contains(transform.parent.gameObject)) m_Chains.Add(transform.parent.gameObject);
        }

        //色の変更の処理
        if (m_Chains.Count >= 3 && mIsHit)
        {
            debugTime += Time.deltaTime;
            if(debugTime > 1)
            {
                debugTime = 0;
                var objs = GameObject.FindGameObjectsWithTag("Collider");
                foreach (GameObject obj in objs)
                {
                    obj.GetComponent<Chain>().m_Chains.Clear();
                }
            }

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

        if (!mIsHit) m_Chains.Clear();

    }

    void OnTriggerStay2D(Collider2D other)
    {
        mIsHit = true;
        if (m_isCheck)
        {
            if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
            {
                var temp = other.transform.FindChild("Collider");
                if (!temp.GetComponent<Chain>().IsCheck())
                {
                    temp.GetComponent<Chain>().SetCheck(true);
                }
            }
        }

        if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
        {
            var temp = other.transform.FindChild("Collider").GetComponent<Chain>();
            this.m_isFlash = true;
            temp.m_isFlash = true;
            temp.m_Chains = this.m_Chains;
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        mIsHit = false;
        if (other.tag == "Block" /*&& ColorChack(other.GetComponent<BlockInfo>().m_ColorState)*/)
        {
            var objs = GameObject.FindGameObjectsWithTag("Collider");
            foreach (GameObject obj in objs)
            {
                obj.GetComponent<Chain>().m_isFlash = false;
                obj.GetComponent<Chain>().m_Chains.Clear();

            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        mIsHit = false;
        if (other.tag == "Block" /*&& ColorChack(other.GetComponent<BlockInfo>().m_ColorState)*/)
        {
            var objs = GameObject.FindGameObjectsWithTag("Collider");
            foreach (GameObject obj in objs)
            {
                obj.GetComponent<Chain>().m_isFlash = false;
                obj.GetComponent<Chain>().m_Chains.Clear();

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

    private bool CheckRemove(GameObject list)
    {
        return !list.transform.FindChild("Collider").GetComponent<Chain>().m_isFlash;
    }
}
