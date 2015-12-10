using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chain : MonoBehaviour
{


    private BlockInfo m_MyInfo;

    /// <summary>
    /// true:チェック済み false::チェックされてない;
    /// </summary>
    private bool m_isCheck = false;

    private bool m_isFlash = false;
    private bool m_isPrevFlash = false;
    //private List<GameObject> m_otherBlocks = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        m_MyInfo = gameObject.transform.parent.GetComponent<BlockInfo>();

    }

    // Update is called once per frame
    void Update()
    {
        if (m_isCheck)
        {
            gameObject.transform.parent.GetComponent<Rigidbody2D>().WakeUp();
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
                    temp.GetComponent<Chain>().SetCheck(true);
                }
            }
        }

        if (m_isFlash)
        {
            if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
            {
                var temp = other.transform.FindChild("Collider");
                if (!temp.GetComponent<Chain>().IsFlash())
                {
                    temp.GetComponent<Chain>().SetFlash(true);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Block" && ColorChack(other.GetComponent<BlockInfo>().m_ColorState))
        {
            var temp = other.transform.FindChild("Collider");
            if (!temp.GetComponent<Chain>().IsFlash())
            {
                temp.GetComponent<Chain>().SetFlash(true);
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

    public void SetFlash(bool flash)
    {
        m_isFlash = flash;
    }

    public bool IsFlash()
    {
        return m_isFlash;
    }
}
